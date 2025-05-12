#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import sys
import os
import re
import json
import unicodedata
import pdfplumber

def strip_accents(s: str) -> str:
    """Elimina tildes y diacríticos para comparar."""
    return ''.join(
        c for c in unicodedata.normalize('NFKD', s)
        if not unicodedata.combining(c)
    )

def extract_header(text: str) -> dict:
    """
    Extrae de la cabecera del PDF (buscando línea a línea):
      - subject
      - code
      - version
      - valid_from
      - practice_number
      - title
    """
    lines = text.splitlines()
    norm  = [strip_accents(ln).upper() for ln in lines]

    header = {
        "subject":         None,
        "code":            None,
        "version":         None,
        "valid_from":      None,
        "practice_number": None,
        "title":           None
    }

    # 1) CODE
    for i, ln_norm in enumerate(norm):
        if ln_norm.strip().startswith("CODIGO:"):
            header["code"] = lines[i].split(":",1)[1].strip()
            break

    # 2) VERSION
    for i, ln_norm in enumerate(norm):
        if ln_norm.strip().startswith("VERSION:"):
            header["version"] = lines[i].split(":",1)[1].strip()
            break

    # 3) SUBJECT + VALID_FROM
    for i, ln_norm in enumerate(norm):
        if ln_norm.strip().startswith("GUIA PRACTICA DE"):
            # línea 1 de subject
            raw1 = lines[i]
            subj1 = re.sub(r'(?i)GUIA PRÁCTICA DE', '', raw1).strip()
            # línea 2 de subject (próxima no vacía)
            j = i+1
            while j < len(lines) and not lines[j].strip():
                j += 1
            raw2 = lines[j]
            # separar antes de "Vigencia"
            subj2 = re.split(r'\s+Vigencia', raw2, flags=re.IGNORECASE)[0].strip()
            header["subject"] = f"{subj1} {subj2}"
            # valid_from
            m = re.search(r'Vigencia(?:\s*desde)?:\s*([0-9]{4}-[0-9]{2}-[0-9]{2})', raw2, re.IGNORECASE)
            if m:
                header["valid_from"] = m.group(1)
            break

    # 4) PRACTICE_NUMBER + TITLE (2 líneas)
    for i, ln_norm in enumerate(norm):
        if ln_norm.strip().startswith("PRACTICA"):
            # número
            m = re.search(r'(\d+)', lines[i])
            if m:
                header["practice_number"] = int(m.group(1))
            # título línea 1
            j = i+1
            while j < len(lines) and not lines[j].strip():
                j += 1
            line1 = lines[j].strip()
            # título línea 2
            k = j+1
            while k < len(lines) and not lines[k].strip():
                k += 1
            line2 = lines[k].strip()
            header["title"] = f"{line1} {line2}"
            break

    return header

def extract_section3(text: str) -> str:
    """Extrae la sección 3: MATERIALES, REACTIVOS Y EQUIPOS."""
    pattern = r'(?:^|\n)3\.\s*(?:MATERIALES(?:, REACTIVOS Y EQUIPOS)?).*?(?=\n4\.)'
    m = re.search(pattern, text, re.IGNORECASE | re.DOTALL)
    return m.group().strip() if m else ""

def parse_section(section_text: str):
    """Parsea sección 3 y devuelve: groups, equipment, supplies."""
    gm = re.search(r'La práctica es para\s*(\d+)\s*grupo', section_text, re.IGNORECASE)
    groups = int(gm.group(1)) if gm else 0

    lines = [l.strip() for l in section_text.splitlines() if l.strip()]
    try:
        i_eq  = lines.index("EQUIPOS")
        i_ins = lines.index("INSUMOS")
    except ValueError:
        i_eq = i_ins = None

    equip_lines = lines[i_eq+1:i_ins] if i_eq is not None and i_ins is not None else []
    sup_lines   = lines[i_ins+1:]      if i_ins is not None else []

    def merge(raw):
        buf, out = "", []
        for ln in raw:
            if re.match(r'^\d+', ln):
                if buf: out.append(buf)
                buf = ln
            else:
                buf += " " + ln
        if buf: out.append(buf)
        return out

    def parse(rows):
        items = []
        for r in merge(rows):
            m = re.match(r'(\d+)\s+(\w+)\s+(.*)', r)
            if m:
                items.append({
                    "quantity_per_group": int(m.group(1)),
                    "unit":               m.group(2),
                    "description":        m.group(3).strip()
                })
        return items

    return groups, parse(equip_lines), parse(sup_lines)

def process_pdf(pdf_path: str) -> dict:
    with pdfplumber.open(pdf_path) as pdf:
        text = "\n".join(page.extract_text() or "" for page in pdf.pages)

    hdr      = extract_header(text)
    sec3     = extract_section3(text)
    groups, equipment, supplies = parse_section(sec3)

    return {
        "file":            os.path.basename(pdf_path),
        "subject":         hdr["subject"],
        "code":            hdr["code"],
        "version":         hdr["version"],
        "valid_from":      hdr["valid_from"],
        "practice_number": hdr["practice_number"],
        "title":           hdr["title"],
        "content":         sec3,
        "groups":          groups,
        "materials": {
            "equipment": equipment or None,
            "supplies":  supplies  or None
        }
    }

def main():
    if len(sys.argv) < 2:
        sys.stderr.write("Usage: python PyAnalyze.py <path_to_pdf>\n")
        sys.exit(1)
    pdf_path = sys.argv[1]
    if not os.path.isfile(pdf_path):
        sys.stderr.write(f"File not found: {pdf_path}\n")
        sys.exit(2)

    try:
        result = process_pdf(pdf_path)
        print(json.dumps(result, ensure_ascii=False, indent=2))
    except Exception as e:
        sys.stderr.write(f"Error: {e}\n")
        sys.exit(3)

if __name__ == "__main__":
    main()

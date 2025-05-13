# -*- coding: utf-8 -*-
import os
import re
import json
import pdfplumber

# Configuración de rutas (ajusta a tu entorno)
INPUT_DIR = r"C:\Users\figue\OneDrive\Escritorio\uflid\forecast-labs\project\labs_f_api\fl_api\ExtracPDF\Input"
OUTPUT_DIR = r"C:\Users\figue\OneDrive\Escritorio\uflid\forecast-labs\project\labs_f_api\fl_api\ExtracPDF\Output"

os.makedirs(OUTPUT_DIR, exist_ok=True)

def extract_section3(text: str) -> str:
    """Extrae la seccion 3 completa del texto."""
    pattern = r'(?:^|\n)3\.\s*(?:MATERIALES(?:, REACTIVOS Y EQUIPOS)?).*?(?=\n4\.)'
    match = re.search(pattern, text, re.S | re.I)
    return match.group().strip() if match else ""

def parse_section(section_text: str):
    """Parsea la seccion 3 y devuelve groups, equipment, supplies."""
    # Extraer numero de grupos
    gm = re.search(r'La practica es para\s*(\d+)\s*grupo', section_text, re.I)
    groups = int(gm.group(1)) if gm else 0

    # Limpiar lineas
    lines = [l.strip() for l in section_text.splitlines() if l.strip()]
    idx_ins = next((i for i,l in enumerate(lines) if re.match(r'INSUMOS', l, re.I)), None)
    try:
        start_eq = lines.index("EQUIPOS") + 1
    except ValueError:
        start_eq = 0
    equip_lines = lines[start_eq:idx_ins] if idx_ins is not None else lines[start_eq:]
    sup_lines   = lines[idx_ins+1:] if idx_ins is not None else []

    def merge_rows(raw_lines):
        rows = []
        buf = ""
        for line in raw_lines:
            if re.match(r'^\d+', line):
                if buf:
                    rows.append(buf)
                buf = line
            else:
                buf += ' ' + line
        if buf:
            rows.append(buf)
        return rows

    equip_rows = merge_rows(equip_lines)
    sup_rows   = merge_rows(sup_lines)

    def parse_rows(rows):
        items = []
        for row in rows:
            m = re.match(r'(\d+)\s+(\w+)\s+(.*)', row)
            if m:
                qty  = int(m.group(1))
                unit = m.group(2)
                desc = m.group(3).strip()
                items.append({
                    "quantity_per_group": qty,
                    "unit": unit,
                    "description": desc
                })
        return items

    return groups, parse_rows(equip_rows), parse_rows(sup_rows)

def process_pdf(pdf_path: str):
    with pdfplumber.open(pdf_path) as pdf:
        text = "\n".join(page.extract_text() or "" for page in pdf.pages)

    section = extract_section3(text)
    groups, equipment, supplies = parse_section(section)

    return {
        "file": os.path.basename(pdf_path),
        "content": section,
        "groups": groups,
        "materials": {
            "equipment": equipment,
            "supplies": supplies
        }
    }

def main():
    # Solo procesa el primer PDF en la carpeta Input
    pdfs = [f for f in os.listdir(INPUT_DIR) if f.lower().endswith(".pdf")]
    if not pdfs:
        print("[Aviso] No hay PDFs en Input.")
        return

    fname    = pdfs[0]
    pdf_path = os.path.join(INPUT_DIR, fname)
    result   = process_pdf(pdf_path)

    # Borrar el PDF de Input
    os.remove(pdf_path)
    print(f"[OK] PDF borrado: {fname}")

    # Guardar JSON resultante
    out = os.path.join(OUTPUT_DIR, "result.json")
    with open(out, "w", encoding="utf-8") as f:
        json.dump(result, f, ensure_ascii=False, indent=2)
    print(f"[OK] JSON generado: {out}")

if __name__ == "__main__":
    main()

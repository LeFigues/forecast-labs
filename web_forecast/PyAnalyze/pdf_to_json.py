#!/usr/bin/env python3
# pdf_to_json.py
# -*- coding: utf-8 -*-

import os
import sys
import json
import pdfplumber

def pdf_to_json(pdf_path: str) -> dict:
    """
    Open the PDF at `pdf_path` and return a dict with:
    {
      "file": "<filename>",
      "pages": [
        { "page_number": 1, "text": "..." },
        { "page_number": 2, "text": "..." },
        ...
      ]
    }
    """
    data = {
        "file": os.path.basename(pdf_path),
        "pages": []
    }
    with pdfplumber.open(pdf_path) as pdf:
        for i, page in enumerate(pdf.pages, start=1):
            text = page.extract_text() or ""
            data["pages"].append({
                "page_number": i,
                "text": text
            })
    return data

def main():
    if len(sys.argv) != 2:
        sys.stderr.write("Usage: python pdf_to_json.py <path_to_pdf>\n")
        sys.exit(1)

    pdf_path = sys.argv[1]
    if not os.path.isfile(pdf_path):
        sys.stderr.write(f"File not found: {pdf_path}\n")
        sys.exit(2)

    result = pdf_to_json(pdf_path)
    print(json.dumps(result, ensure_ascii=False, indent=2))

if __name__ == "__main__":
    main()

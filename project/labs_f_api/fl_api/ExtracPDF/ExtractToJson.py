import os
import re
import pdfplumber

# Directorios de trabajo
INPUT_DIR = "C:\Users\figue\OneDrive\Escritorio\uflid\forecast-labs\project\labs_f_api\fl_api\ExtracPDF\Input"
OUTPUT_DIR = "C:\Users\figue\OneDrive\Escritorio\uflid\forecast-labs\project\labs_f_api\fl_api\ExtracPDF\Output"
# Asegurarnos de que exista la carpeta de salida
os.makedirs(OUTPUT_DIR, exist_ok=True)

def extract_section3(text: str) -> str | None:
    """
    Extrae la sección 3 (Materiales, Equipos, Insumos, Reactivos, etc.)
    de un texto completo, devolviendo el bloque hasta justo antes de la sección 4.
    """
    pattern = r'(?:^|\n)3\.\s*(?:MATERIALES|MATERIALES, REACTIVOS).*?(?=\n4\.)'
    match = re.search(pattern, text, re.S | re.I)
    return match.group().strip() if match else None

def process_pdf(pdf_path: str, output_path: str):
    # Leer y concatenar texto de todas las páginas
    full_text = []
    with pdfplumber.open(pdf_path) as pdf:
        for page in pdf.pages:
            page_text = page.extract_text()
            if page_text:
                full_text.append(page_text)
    full_text = "\n".join(full_text)
    
    # Extraer sección 3
    section3 = extract_section3(full_text)
    if section3:
        with open(output_path, "w", encoding="utf-8") as f_out:
            f_out.write(section3)
        print(f"[OK]   Sección 3 extraída: {os.path.basename(output_path)}")
    else:
        print(f"[Aviso] No se encontró sección 3 en: {os.path.basename(pdf_path)}")

def main():
    # Listar todos los PDFs en input/
    for fname in os.listdir(INPUT_DIR):
        if not fname.lower().endswith(".pdf"):
            continue
        in_path = os.path.join(INPUT_DIR, fname)
        # Cambiar extensión a .txt y escribir en output/
        out_fname = os.path.splitext(fname)[0] + "_seccion3.txt"
        out_path = os.path.join(OUTPUT_DIR, out_fname)
        process_pdf(in_path, out_path)

if __name__ == "__main__":
    """
    Asegúrate de:
      1) Instalar pdfplumber: pip install pdfplumber
      2) Crear las carpetas `input/` y `output/` en el mismo directorio que este script.
      3) Colocar ahí tus PDFs y ejecutar: python extraer_seccion3.py
    """
    main()

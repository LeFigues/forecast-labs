import pdfplumber
import re
import json
import os

# Rutas absolutas
INPUT_FOLDER = r"C:\Users\figue\OneDrive\Escritorio\uflid\forecast-labs\project\labs_f_api\fl_api\ExtracPDF\Input"
OUTPUT_FOLDER = r"C:\Users\figue\OneDrive\Escritorio\uflid\forecast-labs\project\labs_f_api\fl_api\ExtracPDF\Output"
#INPUT_FOLDER = "/var/www/forecastlabs/web_forecast/ExtracPDF/Input"
#OUTPUT_FOLDER = "/var/www/forecastlabs/web_forecast/ExtracPDF/Output"
def parse_materiales(text_block):
    materiales = []
    current = None

    for line in text_block.strip().split("\n"):
        line = line.strip()
        match = re.match(r"^(\d+)\s+Pza\s+(.+)", line)
        if match:
            if current:
                materiales.append(current)
            current = {
                "cantidad_por_grupo": int(match.group(1)),
                "unidad": "Pza",
                "descripcion": match.group(2).strip(),
                "observaciones": None
            }
        elif current:
            if "la práctica es para" in line.lower():
                current["observaciones"] = (current["observaciones"] or "") + " " + line.strip()
            else:
                current["descripcion"] += " " + line.strip()
    if current:
        materiales.append(current)
    return materiales

def extract_materiales_from_pdf(filepath):
    with pdfplumber.open(filepath) as pdf:
        text = "\n".join(page.extract_text() or "" for page in pdf.pages)

    match_lab = re.search(r"PRÁCTICA N[ºo]\s*(\d+)", text, re.IGNORECASE)
    laboratorio = f"LAB-{match_lab.group(1)}" if match_lab else "LAB-UNKNOWN"

    match_title = re.search(r"PRÁCTICA N[ºo]\s*\d+\s*(.+?)\n", text)
    titulo = match_title.group(1).strip() if match_title else "Sin título"

    equipos = []
    match_eq = re.search(r"EQUIPOS\s+(.*?)INSUMOS", text, re.DOTALL)
    if match_eq:
        equipos = parse_materiales(match_eq.group(1))

    insumos = []
    match_in = re.search(r"INSUMOS\s+(.*?)\n4\.", text, re.DOTALL)
    if match_in:
        insumos = parse_materiales(match_in.group(1))

    return {
        "laboratorio": laboratorio,
        "titulo": titulo,
        "grupos": 10,
        "materiales": {
            "equipos": equipos,
            "insumos": insumos
        }
    }

def main():
    archivos_pdf = [f for f in os.listdir(INPUT_FOLDER) if f.lower().endswith('.pdf')]
    if not archivos_pdf:
        print("[!] No se encontró ningún archivo PDF en la carpeta Input.")
        return

    archivo_pdf = archivos_pdf[0]
    ruta_entrada = os.path.join(INPUT_FOLDER, archivo_pdf)
    ruta_salida = os.path.join(OUTPUT_FOLDER, "result.json")

    print(f"[*] Analizando: {archivo_pdf}")
    datos = extract_materiales_from_pdf(ruta_entrada)

    with open(ruta_salida, "w", encoding="utf-8") as f:
        json.dump(datos, f, ensure_ascii=False, indent=2)

    print(f"[OK] Resultado guardado en: {ruta_salida}")

    try:
        os.remove(ruta_entrada)
        print(f"[DEL] Archivo eliminado: {archivo_pdf}")
    except Exception as e:
        print(f"[!] Error al eliminar archivo: {e}")

if __name__ == "__main__":
    main()

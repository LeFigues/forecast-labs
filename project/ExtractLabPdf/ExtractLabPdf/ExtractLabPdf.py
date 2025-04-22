import os
import json
import re

def analizar_materiales_desde_seccion_3(json_path, output_path):
    os.makedirs(output_path, exist_ok=True)
    base_name = os.path.splitext(os.path.basename(json_path))[0]

    with open(json_path, "r", encoding="utf-8") as f:
        data = json.load(f)

    raw_text = " ".join([p.get("raw_text", "") for p in data.get("page_data", [])]).replace("\n", " ")

    # === Recortar desde sección 3 hasta antes de sección 4 ===
    match_materiales = re.search(r"3\.\s+MATERIALES.*?(?=4\.)", raw_text, re.IGNORECASE | re.DOTALL)
    bloque_materiales = match_materiales.group(0) if match_materiales else ""

    # === Buscar número de práctica y título ===
    match_lab = re.search(r"PRACTICA\s+NO\s*(\d+)\s+([A-Z][^\d]+?)\s+\d", raw_text, re.IGNORECASE)
    numero = match_lab.group(1) if match_lab else "N/A"
    titulo = match_lab.group(2).strip().title() if match_lab else "Título no encontrado"

    # === Buscar cantidad de grupos ===
    match_grupos = re.search(r"capacidad\s+del\s+.*?es\s+de\s+(\d+)\s+grupos", bloque_materiales, re.IGNORECASE)
    grupos = int(match_grupos.group(1)) if match_grupos else None

    equipos = []
    insumos = []

    # === Dividir EQUIPOS e INSUMOS ===
    match_ei = re.search(r"EQUIPOS\s+(.*?)INSUMOS\s+(.*)", bloque_materiales, re.IGNORECASE | re.DOTALL)
    if match_ei:
        bloque_equipos = match_ei.group(1)
        bloque_insumos = match_ei.group(2)
    else:
        bloque_equipos, bloque_insumos = "", ""

    # === Analizar insumos con cantidad explícita ===
    lineas_insumos = re.findall(r"(\d+)\s+(Pza|pieza|pz)\s+(.+?)(?=\s+\d+\s+(Pza|pieza|pz)|$)", bloque_insumos, re.IGNORECASE)
    for match in lineas_insumos:
        insumos.append({
            "cantidad_por_grupo": int(match[0]),
            "unidad": "Pza",
            "descripcion": match[2].strip()
        })

    # === Analizar equipos por patrón libre (sin cantidad) ===
    patrones = [
        r"Router\s+Cisco\s+\d{4}.*?(?=Switch|PC|$)",
        r"Switch\s+Cisco\s+\d{4}.*?(?=Router|PC|$)",
        r"PC\s+.*?Tera Term.*?(?=Router|Switch|$)",
        r"PC\s+con\s+emulaci[oó]n.*?(?=Router|Switch|$)"
    ]

    for patron in patrones:
        matches = re.findall(patron, bloque_equipos, re.IGNORECASE)
        for m in matches:
            equipos.append({
                "cantidad_por_grupo": 1,
                "unidad": "Pza",
                "descripcion": m.strip()
            })

    # === Resultado final ===
    resultado = {
        "laboratorio": f"REDES Y COMUNICACIÓN DE DATOS II - PRÁCTICA {numero}",
        "titulo": titulo,
        "grupos": grupos,
        "materiales": {
            "equipos": equipos,
            "insumos": insumos
        }
    }

    # === Guardar JSON estructurado ===
    with open(os.path.join(output_path, f"{base_name}.json"), "w", encoding="utf-8") as f:
        json.dump(resultado, f, indent=2, ensure_ascii=False)

    # === Guardar TXT con materiales ===
    txt_lines = []
    if equipos:
        txt_lines.append("=== EQUIPOS ===")
        for e in equipos:
            txt_lines.append(f"{e['cantidad_por_grupo']} {e['unidad']} - {e['descripcion']}")
    if insumos:
        txt_lines.append("\n=== INSUMOS ===")
        for i in insumos:
            txt_lines.append(f"{i['cantidad_por_grupo']} {i['unidad']} - {i['descripcion']}")
    if not txt_lines:
        txt_lines.append("No se encontraron equipos ni insumos en este documento.")

    with open(os.path.join(output_path, f"{base_name}_materiales.txt"), "w", encoding="utf-8") as f:
        f.write("\n".join(txt_lines))

    print(f"✅ Procesado: {base_name}")
    return resultado


# === EJECUCIÓN PRINCIPAL: analiza todos los .json en outputJson/
if __name__ == "__main__":
    input_folder = "outputJson"
    output_folder = "outputData"

    os.makedirs(input_folder, exist_ok=True)
    os.makedirs(output_folder, exist_ok=True)

    archivos_json = [f for f in os.listdir(input_folder) if f.lower().endswith(".json")]
    if not archivos_json:
        print("❌ No se encontraron archivos JSON en outputJson/")
    else:
        for archivo in archivos_json:
            ruta_json = os.path.join(input_folder, archivo)
            analizar_materiales_desde_seccion_3(ruta_json, output_folder)

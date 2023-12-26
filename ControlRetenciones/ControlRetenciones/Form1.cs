using ClosedXML.Excel;
using SpreadsheetLight;
using System.Globalization;

namespace ControlRetenciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSeleccionarArchivo1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";
                openFileDialog.Title = "Seleccionar primer archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox correspondiente
                    txtRutaArchivo1.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnSeleccionarArchivo2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";
                openFileDialog.Title = "Seleccionar segundo archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox correspondiente
                    txtRutaArchivo2.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            string pathfileArchivo1 = txtRutaArchivo1.Text;
            string pathfileArchivo2 = txtRutaArchivo2.Text;

            CompararArchivos(pathfileArchivo1, pathfileArchivo2);

            // Muestra un mensaje de éxito
            MessageBox.Show("Proceso completado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static void CompararArchivos(string pathfileArchivo1, string pathfileArchivo2)
        {
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe");
                    int colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe Ret./Perc.");
                    int colNroDocArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Nro. Doc.");
                    int colCuitAgenteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "CUIT Agente Ret./Perc.");
                    int colNroCertificadoArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Nro. Certif.");
                    int colNroCertificadoArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Número Certificado");
                    int colFechaArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Fecha");
                    int colFechaArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Fecha Ret./Perc.");


                    List<XLColor> coloresCoincide = new List<XLColor>
                    {
                        XLColor.FromArgb(255, 204, 255, 204), // Tono de verde claro
                    };

                            List<XLColor> coloresNoCoincide = new List<XLColor>
                    {
                        XLColor.FromArgb(255, 255, 204, 204), // Tono de rojo claro
                    };

                    int indiceColor = 0;

                    // Diccionarios para almacenar filas por Nro. Doc. / CUIT
                    Dictionary<long, List<(int, bool)>> diccionarioArchivo1 = new Dictionary<long, List<(int, bool)>>();
                    Dictionary<long, List<(int, bool)>> diccionarioArchivo2 = new Dictionary<long, List<(int, bool)>>();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        string valorCeldaNroDoc = worksheetArchivo1.Cell(filaArchivo1, colNroDocArchivo1).GetString();
                        long nroDoc = long.Parse(valorCeldaNroDoc);

                        if (diccionarioArchivo1.ContainsKey(nroDoc))
                        {
                            diccionarioArchivo1[nroDoc].Add((filaArchivo1, false));
                        }
                        else
                        {
                            diccionarioArchivo1[nroDoc] = new List<(int, bool)> { (filaArchivo1, false) };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        string valorCeldaCuitAgente = worksheetArchivo2.Cell(filaArchivo2, colCuitAgenteArchivo2).GetString();
                        long cuitAgente = long.Parse(valorCeldaCuitAgente);

                        if (diccionarioArchivo2.ContainsKey(cuitAgente))
                        {
                            diccionarioArchivo2[cuitAgente].Add((filaArchivo2, false));
                        }
                        else
                        {
                            diccionarioArchivo2[cuitAgente] = new List<(int, bool)> { (filaArchivo2, false) };
                        }
                    }

                    // Bucle principal para comparar y marcar en verde
                    foreach (var claveArchivo1 in diccionarioArchivo1.Keys.ToList())
                    {
                        if (diccionarioArchivo2.TryGetValue(claveArchivo1, out List<(int, bool)> filasArchivo2))
                        {
                            foreach ((int filaArchivo1, bool comparado) in diccionarioArchivo1[claveArchivo1].ToList())
                            {
                                if (comparado == false) // Solo si aún no se ha comparado esta fila
                                {
                                    decimal importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).GetValue<decimal>();

                                    int ban = 0;

                                    foreach ((int filaArchivo2, bool comparadoArchivo2) in filasArchivo2.ToList())
                                    {
                                        if (comparadoArchivo2 == false) //Solo si aún no se ha comparado esta fila
                                        {
                                            string valorCeldaImporterArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).GetString();
                                            string valorCeldaImporterArchivo2SinComa = valorCeldaImporterArchivo2.Replace(",", ".");
                                            decimal importeArchivo2 = decimal.Parse(valorCeldaImporterArchivo2SinComa, CultureInfo.InvariantCulture);

                                            // Comparar con una tolerancia de ±10
                                            if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                            {
                                                // Obtén el siguiente color de la lista
                                                XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                                worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = color;
                                                worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = color;

                                                // Marcar como comparado
                                                var indiceArchivo1 = diccionarioArchivo1[claveArchivo1].FindIndex(f => f.Item1 == filaArchivo1);
                                                var indiceArchivo2 = diccionarioArchivo2[claveArchivo1].FindIndex(f => f.Item1 == filaArchivo2);

                                                diccionarioArchivo1[claveArchivo1][indiceArchivo1] = (filaArchivo1, true);
                                                diccionarioArchivo2[claveArchivo1][indiceArchivo2] = (filaArchivo2, true);

                                                indiceColor++; // Incrementar el índice de color

                                                ban = 1;

                                                break; // Salir del bucle interno después de encontrar una coincidencia
                                            }
                                        }
                                    }
                                    if (ban == 0)
                                    {
                                        // Obtén el siguiente color de la lista
                                        XLColor color = coloresNoCoincide[indiceColor % coloresCoincide.Count];

                                        worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = color;
                                    }
                                }
                            }
                        }
                    }

                    // Nuevo diccionario para almacenar filas no marcadas en verde por número de certificado
                    Dictionary<string, List<int>> diccionarioCertificadoNoMarcadoArchivo1 = new Dictionary<string, List<int>>();
                    Dictionary<string, List<int>> diccionarioCertificadoNoMarcadoArchivo2 = new Dictionary<string, List<int>>();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        string certificadoArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colNroCertificadoArchivo1).GetString();
                        XLColor colorArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor;

                        if (colorArchivo1 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        // Manipulación del número de certificado para que coincida con el formato del archivo 2
                        string certificadoArchivo1Formateado = certificadoArchivo1.Replace("-", "").Substring(4);

                        if (diccionarioCertificadoNoMarcadoArchivo1.ContainsKey(certificadoArchivo1Formateado))
                        {
                            diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1Formateado].Add(filaArchivo1);
                        }
                        else
                        {
                            diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1Formateado] = new List<int> { filaArchivo1 };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        string certificadoArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colNroCertificadoArchivo2).GetString();
                        XLColor colorArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor;

                        if (colorArchivo2 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioCertificadoNoMarcadoArchivo2.ContainsKey(certificadoArchivo2))
                        {
                            diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo2].Add(filaArchivo2);
                        }
                        else
                        {
                            diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo2] = new List<int> { filaArchivo2 };
                        }
                    }

                    // Bucle adicional para comparar por número de certificado
                    foreach (var certificadoArchivo1 in diccionarioCertificadoNoMarcadoArchivo1.Keys.ToList())
                    {
                        foreach (int filaArchivo1 in diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1].ToList())
                        {
                            decimal importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).GetValue<decimal>();

                            int ban = 0;

                            if (diccionarioCertificadoNoMarcadoArchivo2.TryGetValue(certificadoArchivo1, out List<int> filasCertificadoArchivo2))
                            {
                                foreach (int filaArchivo2 in filasCertificadoArchivo2.ToList())
                                {
                                    string valorCeldaImporterArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).GetString();
                                    string valorCeldaImporterArchivo2SinComa = valorCeldaImporterArchivo2.Replace(",", ".");
                                    decimal importeArchivo2 = decimal.Parse(valorCeldaImporterArchivo2SinComa, CultureInfo.InvariantCulture);

                                    // Comparar con una tolerancia de ±10
                                    if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                    {
                                        // Obtén el siguiente color de la lista
                                        XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                        worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = color;
                                        worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = color;

                                        // Marcar como comparado
                                        diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1].Remove(filaArchivo1);
                                        diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo1].Remove(filaArchivo2);

                                        indiceColor++; // Incrementar el índice de color

                                        ban = 1;
                                        break; // Salir del bucle interno después de encontrar una coincidencia
                                    }
                                }
                            }

                            if (ban == 0)
                            {
                                // Obtén el siguiente color de la lista
                                XLColor color = coloresNoCoincide[indiceColor % coloresNoCoincide.Count];

                                worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = color;
                            }
                        }
                    }

                    // Bucle adicional para marcar en rojo los registros en el archivo 2 que no están marcados en verde en el archivo 1
                    foreach (var certificadoArchivo2 in diccionarioCertificadoNoMarcadoArchivo2.Keys)
                    {
                        foreach (int filaArchivo2 in diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo2])
                        {
                            // Obtén el siguiente color de la lista para marcar en rojo
                            XLColor colorRojo = coloresNoCoincide[indiceColor % coloresNoCoincide.Count];

                            worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = colorRojo;
                        }
                    }

                    // Diccionarios para almacenar filas marcadas en rojo por fecha
                    Dictionary<DateTime, List<int>> diccionarioFechaArchivo1 = new Dictionary<DateTime, List<int>>();
                    Dictionary<DateTime, List<int>> diccionarioFechaArchivo2 = new Dictionary<DateTime, List<int>>();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        string stringFechaArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colFechaArchivo1).GetString();
                        DateTime fechaArchivo1 = DateTime.Parse(stringFechaArchivo1);

                        XLColor colorArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor;

                        if (colorArchivo1 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioFechaArchivo1.ContainsKey(fechaArchivo1))
                        {
                            diccionarioFechaArchivo1[fechaArchivo1].Add(filaArchivo1);
                        }
                        else
                        {
                            diccionarioFechaArchivo1[fechaArchivo1] = new List<int> { filaArchivo1 };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        string stringFechaArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colFechaArchivo2).GetString();
                        DateTime fechaArchivo2 = DateTime.Parse(stringFechaArchivo2);

                        XLColor colorArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor;

                        if (colorArchivo2 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioFechaArchivo2.ContainsKey(fechaArchivo2))
                        {
                            diccionarioFechaArchivo2[fechaArchivo2].Add(filaArchivo2);
                        }
                        else
                        {
                            diccionarioFechaArchivo2[fechaArchivo2] = new List<int> { filaArchivo2 };
                        }
                    }

                    // Bucle adicional para comparar por fecha e importe y marcar en verde si coinciden
                    foreach (var fechaArchivo1 in diccionarioFechaArchivo1.Keys)
                    {
                        foreach (int filaArchivo1 in diccionarioFechaArchivo1[fechaArchivo1])
                        {
                            decimal importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).GetValue<decimal>();

                            if (diccionarioFechaArchivo2.TryGetValue(fechaArchivo1, out List<int> filasFechaArchivo2))
                            {
                                foreach (int filaArchivo2 in filasFechaArchivo2.ToList())
                                {

                                    decimal importeArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).GetValue<decimal>();

                                    // Comparar por importe
                                    if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                    {
                                        // Obtén el siguiente color de la lista para marcar en verde
                                        XLColor colorVerde = coloresCoincide[indiceColor % coloresCoincide.Count];

                                        worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = colorVerde;
                                        worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = colorVerde;

                                        indiceColor++; // Incrementar el índice de color

                                        break; // Salir del bucle interno después de encontrar una coincidencia
                                    }
                                }
                            }
                        }
                    }

                    // Guardar ambos archivos
                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        
        // Función auxiliar para obtener el índice de la columna por su nombre
        static int ObtenerIndiceColumna(IXLWorksheet worksheet, string nombreColumna)
        {
            int indiceColumna = -1;

            for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
            {
                string valor = worksheet.Cell(1, col).GetString();

                if (valor.Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                {
                    indiceColumna = col;
                    break;
                }
            }

            return indiceColumna;
        }
    }
}
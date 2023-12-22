using ClosedXML.Excel;
using SpreadsheetLight;

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
                    Dictionary<long, List<int>> diccionarioArchivo1 = new Dictionary<long, List<int>>();
                    Dictionary<long, List<int>> diccionarioArchivo2 = new Dictionary<long, List<int>>();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        string valorCeldaNroDoc = worksheetArchivo1.Cell(filaArchivo1, colNroDocArchivo1).GetString();
                        long nroDoc = long.Parse(valorCeldaNroDoc);

                        if (diccionarioArchivo1.ContainsKey(nroDoc))
                        {
                            diccionarioArchivo1[nroDoc].Add(filaArchivo1);
                        }
                        else
                        {
                            diccionarioArchivo1[nroDoc] = new List<int> { filaArchivo1 };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        string valorCeldaCuitAgente = worksheetArchivo2.Cell(filaArchivo2, colCuitAgenteArchivo2).GetString();
                        long cuitAgente = long.Parse(valorCeldaCuitAgente);

                        if (diccionarioArchivo2.ContainsKey(cuitAgente))
                        {
                            diccionarioArchivo2[cuitAgente].Add(filaArchivo2);
                        }
                        else
                        {
                            diccionarioArchivo2[cuitAgente] = new List<int> { filaArchivo2 };
                        }
                    }

                    // Bucle principal para comparar y marcar en verde
                    foreach (var claveArchivo1 in diccionarioArchivo1.Keys)
                    {
                        if (diccionarioArchivo2.TryGetValue(claveArchivo1, out List<int> filasArchivo2))
                        {
                            foreach (int filaArchivo1 in diccionarioArchivo1[claveArchivo1])
                            {
                                decimal importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).GetValue<decimal>();

                                foreach (int filaArchivo2 in filasArchivo2)
                                {
                                    decimal importeArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).GetValue<decimal>();

                                    // Comparar con una tolerancia de ±10
                                    if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                    {
                                        // Obtén el siguiente color de la lista
                                        XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                        worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = color;
                                        worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = color;
                                    }
                                }
                            }
                        }
                    }

                    // Bucle adicional para comparar los que no están marcados en verde
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        if (!EsFilaMarcadaEnVerde(worksheetArchivo1, filaArchivo1, colImporteArchivo1))
                        {
                            decimal importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).GetValue<decimal>();

                            // Verificar solo las filas no marcadas en verde en el archivo 2
                            for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                            {
                                if (!EsFilaMarcadaEnVerde(worksheetArchivo2, filaArchivo2, colImporteArchivo2))
                                {
                                    decimal importeArchivo2 = worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).GetValue<decimal>();

                                    // Comparar con una tolerancia de ±10
                                    if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                    {
                                        // Obtén el siguiente color de la lista
                                        XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                        worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = color;
                                        worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = color;
                                    }
                                }
                            }
                        }
                    }


                    // Bucle para marcar en rojo los que no están marcados en verde en el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        if (!EsFilaMarcadaEnVerde(worksheetArchivo1, filaArchivo1, colImporteArchivo1))
                        {
                            worksheetArchivo1.Cell(filaArchivo1, colImporteArchivo1).Style.Fill.BackgroundColor = coloresNoCoincide[0];
                        }
                    }

                    // Bucle para marcar en rojo los que no están marcados en verde en el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        if (!EsFilaMarcadaEnVerde(worksheetArchivo2, filaArchivo2, colImporteArchivo2))
                        {
                            worksheetArchivo2.Cell(filaArchivo2, colImporteArchivo2).Style.Fill.BackgroundColor = coloresNoCoincide[0];
                        }
                    }

                    // Guardar el archivo "agrupado" después de marcar en verde y en rojo
                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        static bool EsFilaMarcadaEnVerde(IXLWorksheet worksheet, int fila, int columna)
        {
            return worksheet.Cell(fila, columna).Style.Fill.BackgroundColor.Equals(XLColor.FromArgb(255, 204, 255, 204));
        }



        // Función auxiliar para obtener el índice de la columna por su nombre
        static int ObtenerIndiceColumna(SLDocument workbook, string nombreColumna)
        {
            int indiceColumna = -1;
            SLWorksheetStatistics statistics = workbook.GetWorksheetStatistics();

            string[] palabras = nombreColumna.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int col = 1; col <= statistics.EndColumnIndex; col++)
            {
                string valor = workbook.GetCellValueAsString(1, col);

                if (palabras.Any(palabra => valor.Contains(palabra, StringComparison.OrdinalIgnoreCase)))
                {
                    // Entra aquí si alguna palabra coincide con el valor
                    indiceColumna = col;
                    break;
                }
            }

            return indiceColumna;
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
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
                                                break; // Salir del bucle interno después de encontrar una coincidencia
                                            }
                                        }                                        
                                    }
                                }
                            }
                        }
                    }

                    //guardar ambos archivos
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

        private void txtRutaArchivo1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSeleccionarArchivo2_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using DocumentFormat.OpenXml.Presentation;

namespace ControlRetenciones
{
    public partial class Form1 : Form
    {
        List<XLColor> coloresCoincide = new List<XLColor>
        {
            XLColor.FromArgb(255, 204, 255, 204), // Tono de verde claro
        };

        List<XLColor> coloresNoCoincide = new List<XLColor>
        {
            XLColor.FromArgb(255, 255, 204, 204), // Tono de rojo claro
        };
        public Form1()
        {
            InitializeComponent();

            // Establecer el estilo del borde y deshabilitar el cambio de tamaño
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Establecer el tamaño mínimo y máximo para evitar el cambio de tamaño
            this.MinimumSize = this.MaximumSize = this.Size;

            // Ocultar el PictureBox al iniciar
            pictureBoxRuedaCargando.Visible = false;
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

        private async void btnProcesar_Click(object sender, EventArgs e)
        {
            // Mostrar el PictureBox antes de comenzar el proceso
            pictureBoxRuedaCargando.Visible = true;

            string pathfileArchivo1 = txtRutaArchivo1.Text;
            string pathfileArchivo2 = txtRutaArchivo2.Text;

            // Convertir archivos XLS a XLSX si es necesario
            if (Path.GetExtension(pathfileArchivo1).ToLower() == ".xls")
            {
                pathfileArchivo1 = ConvertXlsToXlsx(pathfileArchivo1);
            }

            if (Path.GetExtension(pathfileArchivo2).ToLower() == ".xls")
            {
                pathfileArchivo2 = ConvertXlsToXlsx(pathfileArchivo2);
            }

            // Realizar el proceso de manera asíncrona
            await Task.Run(() => CompararArchivos(pathfileArchivo1, pathfileArchivo2));

            // Ocultar el PictureBox al finalizar el proceso
            pictureBoxRuedaCargando.Visible = false;

            // Muestra un mensaje de éxito
            MessageBox.Show("Proceso completado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //codigo para convertir en XLSX los archivos XLS (el que se baja desde AFIP)
        private string ConvertXlsToXlsx(string xlsFilePath)
        {
            // Ruta para guardar el archivo XLSX
            string xlsxFilePath = Path.ChangeExtension(xlsFilePath, ".xlsx");

            // Crear un nuevo libro de trabajo XLSX
            using (var workbookXlsx = new XSSFWorkbook())
            {
                // Abrir el archivo XLS
                using (var fs = new FileStream(xlsFilePath, FileMode.Open, FileAccess.Read))
                {
                    
                    var workbookXls = new HSSFWorkbook(fs);

                    // Copiar todas las hojas de trabajo de XLS a XLSX
                    for (int i = 0; i < workbookXls.NumberOfSheets; i++)
                    {
                        var sheetXls = workbookXls.GetSheetAt(i);
                        var sheetXlsx = workbookXlsx.CreateSheet(sheetXls.SheetName);

                        for (int row = 0; row <= sheetXls.LastRowNum; row++)
                        {
                            var rowXls = sheetXls.GetRow(row);
                            var rowXlsx = sheetXlsx.CreateRow(row);

                            if (rowXls != null)
                            {
                                for (int col = 0; col <= rowXls.LastCellNum; col++)
                                {
                                    var cellXls = rowXls.GetCell(col);
                                    var cellXlsx = rowXlsx.CreateCell(col);

                                    if (cellXls != null)
                                    {
                                        switch (cellXls.CellType)
                                        {
                                            case NPOI.SS.UserModel.CellType.Numeric:
                                                cellXlsx.SetCellValue(cellXls.NumericCellValue);
                                                break;
                                            case NPOI.SS.UserModel.CellType.String:
                                                cellXlsx.SetCellValue(cellXls.StringCellValue);
                                                break;
                                                // Agregar otros casos según sea necesario
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Guardar el archivo XLSX
                using (var fs = new FileStream(xlsxFilePath, FileMode.Create, FileAccess.Write))
                {
                    workbookXlsx.Write(fs);
                }
            }

            return xlsxFilePath;
        }

        private void CompararArchivos(string pathfileArchivo1, string pathfileArchivo2)
        {
            // Llamadas a funciones específicas
            CompararArchivosPorCuit(pathfileArchivo1, pathfileArchivo2);
            CompararArchivosPorCertificado(pathfileArchivo1, pathfileArchivo2);
            CompararArchivosPorFechaEImporte(pathfileArchivo1, pathfileArchivo2);

            //Marcar en rojo los que no coinciden
            MarcarNoCoincidentesEnRojo(pathfileArchivo1);
            MarcarNoCoincidentesEnRojo(pathfileArchivo2);
        }

        private void CompararArchivosPorCuit(string pathfileArchivo1, string pathfileArchivo2)
        {             
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe");
                    if (colImporteArchivo1 == -1)
                    {
                        colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "ImporteRet");
                    }
                    if (colImporteArchivo1 == -1)
                    {
                        colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe Ret./Perc.");
                    }
                    int colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe Ret./Perc.");
                    if (colImporteArchivo2 == -1)
                    {
                        colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "ImporteRet");
                    }
                    if (colImporteArchivo2 == -1)
                    {
                        colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe");
                    }
                    int colNroDocArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Nro. Doc.");
                    if (colNroDocArchivo1 == -1)
                    {
                        colNroDocArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Cuit");
                    }
                    if (colNroDocArchivo1 == -1)
                    {
                        colNroDocArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "CUIT Agente Ret./Perc.");
                    }
                    int colCuitAgenteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "CUIT Agente Ret./Perc.");
                    if (colCuitAgenteArchivo2 == -1)
                    {
                        colCuitAgenteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Cuit");
                    }
                    if (colCuitAgenteArchivo2 == -1)
                    {
                        colCuitAgenteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Nro. Doc.");
                    }

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


                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        private void CompararArchivosPorCertificado(string pathfileArchivo1,string pathfileArchivo2)
        {
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int indiceColor = 0;

                    int colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe");
                    if (colImporteArchivo1 == -1)
                    {
                        colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "ImporteRet");
                    }
                    if (colImporteArchivo1 == -1)
                    {
                        colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe Ret./Perc.");
                    }
                    int colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe Ret./Perc.");
                    if (colImporteArchivo2 == -1)
                    {
                        colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "ImporteRet");
                    }
                    if (colImporteArchivo2 == -1)
                    {
                        colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe");
                    }
                    int colNroCertificadoArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Nro. Certif.");
                    if (colNroCertificadoArchivo1 == -1)
                    {
                        colNroCertificadoArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Certificado");
                    }
                    if (colNroCertificadoArchivo1 == -1)
                    {
                        colNroCertificadoArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Número Certificado");
                    }
                    int colNroCertificadoArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Número Certificado");
                    if (colNroCertificadoArchivo2 == -1)
                    {
                        colNroCertificadoArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Nro. Certif.");
                    }
                    if (colNroCertificadoArchivo2 == -1)
                    {
                        colNroCertificadoArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Certificado");
                    }

                    if (colNroCertificadoArchivo1 == -1 || colNroCertificadoArchivo2 == -1)
                    {
                        return;
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

                    // Guardar el archivo después de la comparación
                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        private void CompararArchivosPorFechaEImporte(string pathfileArchivo1, string pathfileArchivo2)
        {
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int indiceColor = 0;

                    int colFechaArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Fecha");
                    if (colFechaArchivo1 == -1)
                    {
                        colFechaArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Fecha Ret./Perc.");
                    }
                    int colFechaArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Fecha Ret./Perc.");
                    if (colFechaArchivo2 == -1)
                    {
                        colFechaArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Fecha");
                    }
                    int colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe");
                    if (colImporteArchivo1 == -1)
                    {
                        colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "ImporteRet");
                    }
                    if (colImporteArchivo1 == -1)
                    {
                        colImporteArchivo1 = ObtenerIndiceColumna(worksheetArchivo1, "Importe Ret./Perc.");
                    }
                    int colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe Ret./Perc.");
                    if (colImporteArchivo2 == -1)
                    {
                        colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "Importe");
                    }
                    if (colImporteArchivo2 == -1)
                    {
                        colImporteArchivo2 = ObtenerIndiceColumna(worksheetArchivo2, "ImporteRet");
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

                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        private void MarcarNoCoincidentesEnRojo(string pathArchivo)
        {
            using (var workbookArchivo = new XLWorkbook(pathArchivo))
            {
                var worksheetArchivo = workbookArchivo.Worksheets.First();
                int indiceColor = 0;

                int colImporte = ObtenerIndiceColumna(worksheetArchivo, "Importe");
                if (colImporte == -1)
                {
                    colImporte = ObtenerIndiceColumna(worksheetArchivo, "ImporteRet");
                }
                if (colImporte == -1)
                {
                    colImporte = ObtenerIndiceColumna(worksheetArchivo, "Importe Ret./Perc.");
                }

                for (int fila = 2; fila <= worksheetArchivo.RowsUsed().Count(); fila++)
                {
                    var colorArchivo = worksheetArchivo.Cell(fila, colImporte).Style.Fill.BackgroundColor;

                    if (colorArchivo != XLColor.FromArgb(255, 204, 255, 204))
                    {

                        // Obtén el siguiente color de la lista para marcar en rojo
                        XLColor colorRojo = coloresNoCoincide[indiceColor % coloresCoincide.Count];

                        // Marcar en rojo en el archivo
                        worksheetArchivo.Cell(fila, colImporte).Style.Fill.BackgroundColor = colorRojo;

                        indiceColor++; // Incrementar el índice de color
                    }
                }

                // Guardar el archivo
                workbookArchivo.SaveAs(pathArchivo);
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
        private void txtRutaArchivo1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
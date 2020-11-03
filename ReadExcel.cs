//install nugets ExcelDataReader and ExcelDataReader.DataSet
using ExcelDataReader;

static void using ReadExcel()
{
            string filePath = @"C:\Users\UserName\Desktop\a.xlsx";

            // var oppilaat = new List<object>();

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)

                //// reader.IsFirstRowAsColumnNames
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(conf);

                    var sr = result.Tables[0].AsEnumerable().GetEnumerator();

                    //Console.WriteLine("Etunimi\tSukunimi\tSähköposti");

                    while (sr.MoveNext())
                    {
                        var dr = sr.Current.ItemArray;
                        //Console.WriteLine(dr[0] + "\t" + dr[1] + "\t" + dr[2]);
                        oppilaat.Add(new Oppilas { 
                                Etunimi = dr[0] as string,
                                Sukunimi = dr[1] as string, 
                                Sahkoposti = dr[2] as string 
                        });

                    }
                    /* This is one way how to add to db :P
                     * Datacontect prbly EntityFramework db
                     * 
                    Using(Datacontext dc = new Datacontext())
                    {
                        dc.Oppilas.InsertAllOnSubmit(oppilaat);

                        //idk if nesting Using-statements is wise? but transaction might be good idea for larger set of records
                        Using (TransactionScope ts = new TransactionScope())
                        {
                            dc.SubmitChanges();
                         }
                    }
                    */
                    ///LIST<MyDataModel> LINQ to DB
                    //https://stackoverflow.com/questions/9096767/insert-list-of-objects-with-linq-to-sql
                }
            }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CRUD.Models;
using OfficeOpenXml;
using System.IO;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc.Filters;
using ExcelDataReader.Exceptions;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly SheetContext _context;

        public UploadFileController(SheetContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<ActionResult<Sheet1>> PostSheet()
        {
            try
            {
                await UploadFile();
            }            
            catch(FileNotFoundException)
            {
                return BadRequest("Ошибка чтения файла");
            }
            catch(Exception )
            {
                return BadRequest("Ошибка добавления данных из файла");
            }
            return Ok("Данные добавлены успешно");
        }

        private async Task UploadFile()
        {
            string res = "";
            using (FileStream stream = new FileStream("Книга1.xlsx", FileMode.Open))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int j = -1;
                    do
                    {
                        j++;
                        int i = 0;
                        string[] colName = new string[reader.FieldCount];
                        while (reader.Read())
                        {

                            for (int column = 0; column < reader.FieldCount; column++)
                            {
                                Console.WriteLine(reader.GetValue(column));
                                if (i == 0)
                                {
                                    colName[column] = reader.GetValue(column).ToString();
                                }
                                else
                                {
                                    Sheet sheet;
                                    
                                    if (j == 0)
                                    {
                                        sheet = new Sheet1()
                                        {
                                            ColumnName = colName[column],
                                            Value = reader.GetValue(column).ToString(),
                                            DateTime = DateTime.Now
                                        };
                                        await _context.Sheet1s.AddAsync((Sheet1)sheet);
                                    }
                                    else
                                    {
                                        sheet = new Sheet2()
                                        {
                                            ColumnName = colName[column],
                                            Value = reader.GetValue(column).ToString(),
                                            DateTime = DateTime.Now
                                        };
                                        await _context.Sheet2s.AddAsync((Sheet2)sheet);
                                    }
                                }
                            }
                            await _context.SaveChangesAsync();
                            i++;
                        }
                        j++;
                    } while (reader.NextResult());

                }

            }
        }

        
    }
}

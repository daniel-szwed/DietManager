﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public class FileService : ITransferService
    {

        public async Task<IEnumerable<T>> ImportAsync<T>()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "JSON Files(*.json)|*.json|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(dialog.FileName))
                {
                    var serializedData = await reader.ReadToEndAsync();
                    return JsonSerializer.Deserialize<IEnumerable<T>>(serializedData);
                }
            }
            return null;
        }

        public async void ExportAsync<T>(IEnumerable<T> collection)
        {
            var json = JsonSerializer.Serialize(collection);
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "JSON Files(*.json)|*.json|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    await writer.WriteLineAsync(json);
                }
            }
        }
    }
}

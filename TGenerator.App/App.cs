using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4Processor;
using TGenerator.Model;
using TGenerator.TextTransformation;

namespace TGenerator.App
{
    public class App
    {
        private TemplateProcessor _Processor;

        public string TemplatePath { get; set; }

        public string SettingsPath { get; set; }

        public string OutputPath { get; set; }        

        public App()
        {
            this._Processor = TemplateProcessorFactory.DefaultFactory().CreateTemplateProcessor();
        }

        public void Execute() 
        {
            var entities = JsonConvert.DeserializeObject<Entity[]>(File.ReadAllText(this.SettingsPath));

            var allTemplates = Directory.EnumerateFiles(TemplatePath, "*.tt", SearchOption.AllDirectories);

            var singleEntityTemplates = from template in allTemplates
                                        where template.Contains(TGeneratorTextTransformation.ENTITY_FILENAME)
                                        select template;

            singleEntityTemplates.ToList().ForEach(template => entities.ToList().ForEach(entity => ProcessTemplate(template, entity)));

            var multipleEntityTemplates = from template in allTemplates
                                          where !template.Contains(TGeneratorTextTransformation.ENTITY_FILENAME)
                                          select template;

            multipleEntityTemplates.ToList().ForEach(template => ProcessTemplate(template, entities));
        }

        private void ProcessTemplate(string template, Entity entity)
        {
            var processedString = _Processor.ProcessTemplate(template, new Dictionary<string, object> 
            {  
                { TGeneratorTextTransformation.ENTITY_PARAMETER, entity }
            });

            var outputFilename = GetOutputFilename(template, entity);

            if (!Directory.Exists(Path.GetDirectoryName(outputFilename))) Directory.CreateDirectory(Path.GetDirectoryName(outputFilename));

            File.WriteAllText(outputFilename, processedString);
        }

        private void ProcessTemplate(string template, Entity[] entities)
        {
            var processedString = _Processor.ProcessTemplate(template, new Dictionary<string, object> 
            {  
                { TGeneratorTextTransformation.ENTITIES_PARAMETER, entities }
            });

            var outputFilename = GetOutputFilename(template);

            if (!Directory.Exists(Path.GetDirectoryName(outputFilename))) Directory.CreateDirectory(Path.GetDirectoryName(outputFilename));

            File.WriteAllText(outputFilename, processedString);
        }

        private string GetOutputFilename(string templateFilename)
        {
            var templateName = Path.GetFileNameWithoutExtension(templateFilename);
            var templateDirectory = Path.GetDirectoryName(templateFilename);

            var outputPath = this.OutputPath + templateDirectory.Replace(TemplatePath, string.Empty);
            var outputFilename = templateName + _Processor.FileExtension;

            return Path.Combine(outputPath, outputFilename);
        }

        private string GetOutputFilename(string templateFilename, Entity entity)
        {
            var templateName = Path.GetFileNameWithoutExtension(templateFilename);
            var templateDirectory = Path.GetDirectoryName(templateFilename);

            var outputPath = this.OutputPath + templateDirectory.Replace(TemplatePath, string.Empty);
            var outputFilename = templateName.Replace(TGeneratorTextTransformation.ENTITY_FILENAME, entity.Name) + _Processor.FileExtension;

            return Path.Combine(outputPath, outputFilename);
        }
    }
}

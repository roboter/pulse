using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulse.Base;
using System.IO;
using Pulse.Base.Providers;

namespace LocalDirectory
{
    [System.ComponentModel.Description("Local Directory")]
    [ProviderConfigurationClass(typeof(LocalDirectorySettings))]
    [ProviderIcon(typeof(Properties.Resources),"folder")]
    public class Provider : IInputProvider
    {
        public PictureList GetPictures(PictureSearch ps)
        {
            PictureList pl = new PictureList() { FetchDate = DateTime.Now };

            LocalDirectorySettings lds = string.IsNullOrEmpty(ps.SearchProvider.ProviderConfig) ? new LocalDirectorySettings() : LocalDirectorySettings.LoadFromXML(ps.SearchProvider.ProviderConfig);

            //get all files in directory and filter for image extensions (jpg/jpeg/png/bmp?)
            List<string> files = new List<string>();

            foreach (string ext in lds.Extensions.Split(new char[] { ';' }))
            {
                files.AddRange(Directory.GetFiles(lds.Directory, "*." + ext, lds.IncludeSubdirectories?SearchOption.AllDirectories:SearchOption.TopDirectoryOnly));
            }

            //distinct list (just in case)
            files = files.Distinct().ToList();

            var maxPictureCount = ps.MaxPictureCount > 0 ? ps.MaxPictureCount : int.MaxValue;
            maxPictureCount = Math.Min(files.Count, maxPictureCount);


            //create picture items
            pl.Pictures.AddRange((from c in files
                                 select new Picture()
                                 {
                                     Id = Path.GetFileNameWithoutExtension(c),
                                     Url=c,
                                     LocalPath = c
                                 })
                                 .OrderBy(x=>Guid.NewGuid())
                                 .Take(maxPictureCount));

            return pl;
        }

        public void Activate(object args)
        {

        }

        public void Initialize(object args)
        {

        }

        public void Deactivate(object args)
        {
            
        }
    }
}

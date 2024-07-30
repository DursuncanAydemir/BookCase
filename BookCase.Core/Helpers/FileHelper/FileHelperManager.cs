using BookCase.Core.Helpers.FileHelper.Constant;
using BookCase.Core.Helpers.GuidHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Core.Helpers.FileHelper;

public class FileHelperManager : IFileHelper
{
    public string Add(IFormFile file)
    {
        //dosyanın uzantısını alıyorum.
        string fileExtension = Path.GetExtension(file.FileName);
        //Guid ile uzantıyı birleştiriyorum.
        string uniqueFileName = GuidHelper_.Create() + fileExtension;
        //kaydetmek istediğim yerin tam yolunu alıyorum.
        var imagePath = FilePath.Full(uniqueFileName);
        using FileStream fileStream = new(imagePath, FileMode.Create);
        // FilMode.Create
        // İşletim sisteminin yeni bir dosya oluşturması gerektiğini 
        // belirtir. Dosya zaten mevcutsa, üzerine yazılacaktır.
        file.CopyTo(fileStream); //yola kopyalıyorum.
        fileStream.Flush(); // ara belleği temizliyorum.
        return uniqueFileName;
    }

    public void Delete(string path)
    {
        //klasörümde dosyamın olup olmadığını kontrol ediyorum.
        if (Path.Exists(FilePath.Full(path)))
        {
            File.Delete(FilePath.Full(path));
        }
        else
        {
            throw new DirectoryNotFoundException(Messages.FileNotFound);
        }
    }

    public void Update(IFormFile file, string imagePath)
    {
        //bu kısımda var olan guid'i silip eklemek yerine
        //guid yapısı aynı sadece dosyayı değiştiriyorum.
        var fullpath = FilePath.Full(imagePath);
        if (Path.Exists(fullpath))
        {
            using FileStream fileStream = new(fullpath, FileMode.Create);
            //FileMode.Create burada üzerine yazma işlemi yapar.
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        else
        {
            throw new DirectoryNotFoundException(Messages.FileNotFound);
        }

    }
}

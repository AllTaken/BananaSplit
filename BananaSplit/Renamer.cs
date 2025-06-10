using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BananaSplit;
public class Renamer(Settings settings)
{
    public bool RenameOriginalIfWanted(ref string encodingFileName)
    {
        if (!settings.RenameOriginal)
            return true;

        var fileInfo = new FileInfo(encodingFileName);
        var path = Path.GetDirectoryName(encodingFileName);
        var name = Path.GetFileNameWithoutExtension(encodingFileName);
        var ext = Path.GetExtension(encodingFileName);

        encodingFileName = Path.Combine(path, name + "_original" + ext);
        try
        {
            fileInfo.MoveTo(encodingFileName);
        }
        catch
        {
            MessageBox.Show($"There was an error renaming the original file: {encodingFileName}\nMake sure it's not being used by another process!", "Error", MessageBoxButtons.OK);
            return false;
        }

        return true;
    }

    public string GetNewName(string fileName, int index)
    {
        var path = Path.GetDirectoryName(fileName);
        var name = Path.GetFileNameWithoutExtension(fileName);
        var extension = Path.GetExtension(fileName);
        var original = name;

        var oldText = settings.RenameFindText;
        var newText = settings.RenameNewText;
        name = RenameEpisode(index, name, oldText, newText);

        // Add the index if necessary
        name = AddIndexToName(index, name);

        // Make sure the name is different
        if (name == original)
        {
            name += "-" + index;
        }

        var newName = Path.Combine(path, name + extension);

        // Rename again if there's already a file with that name
        if (File.Exists(newName))
        {
            newName = Path.Combine(path, name + DateTimeOffset.Now.ToUnixTimeSeconds() + extension);
        }

        return newName;
    }

    private string RenameEpisode(int index, string name, string oldText, string newText)
    {
        switch (settings.RenameType)
        {
            case RenameType.Prefix:
                name = newText + name;
                break;
            case RenameType.Suffix:
                name += newText;
                break;
            case RenameType.AppendAfter:
                var regex = new Regex(Regex.Escape(oldText));
                name = regex.Replace(name, $"{oldText}{newText}", 1);
                break;
            case RenameType.Replace:
                name = name.Replace(oldText, newText);
                break;
            case RenameType.Increment:
                name = RenameWithIncrement(index, name);
                break;
        }

        return name;
    }

    private string RenameWithIncrement(int index, string name)
    {
        //TODO: Let user input this
        string numPattern = @"(S\d{2,}E)(?'num'\d{2,})(-E\d{2,})?";
        name = Regex.Replace(
            name,
            numPattern,
            match =>
            {
                // Original episode number before renaming
                var num = int.Parse(match.Groups["num"].Value);
                // e.g. S02E
                var season = match.Groups[1].Value;

                //Simple increment
                if (settings.IncrementMultiplier == 0)
                    return season + (num + index - 1).ToString("D2");


                return season +
                    (
                        ((num - 1) * settings.IncrementMultiplier +
                        ((index - 1) % settings.IncrementMultiplier)) +
                        1
                    ).ToString("D2");
            }
        );
        return name;
    }

    private string AddIndexToName(int index, string name)
    {
        if (name.Contains("{i}"))
        {
            if (settings.StartIndex == 0)
            {
                name = name.Replace("{i}", "" + index.ToString().PadLeft(settings.Padding, '0'));
            }
            else
            {
                name = name.Replace("{i}", "" + (settings.StartIndex + index - 1).ToString().PadLeft(settings.Padding, '0'));
            }

        }

        return name;
    }
}

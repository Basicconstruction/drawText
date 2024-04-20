using System.Drawing;

namespace Character_Image.Models;

public interface ICharacterDrawer
{
    Image Draw(string path, GenerateConfig config);
    Image DrawOne(string path, GenerateConfig config);
}
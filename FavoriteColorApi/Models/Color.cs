using FavoriteColorApi.Services;

namespace FavoriteColorApi.Models
{
    public class Color
    {
        private readonly IColorNameProvider _colorNameProvider;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        Color(int id, IColorNameProvider colorNameProvider)
        {
            this._colorNameProvider = colorNameProvider;

            this.Id = id;
            this.Name = this._colorNameProvider.GetColorName(id);
        }
    }
}

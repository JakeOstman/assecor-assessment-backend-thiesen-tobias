namespace FavoriteColorApi.Services
{
    public class ColorNameProvider : IColorNameProvider
    {
        public enum EColorName
        {
            blau = 1,
            grün,
            violett,
            rot,
            gelb,
            türkis,
            weiß
        }

        public string GetColorName(int id)
        {
            if (Enum.IsDefined(typeof(EColorName), id))
            {
                return ((EColorName)id).ToString();
            }

            return "unbekannt";
        }
    }
}

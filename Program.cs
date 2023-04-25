

namespace Poe_show_buff
{
    
    internal static class Program
    {
        static Save save = new Save();
        static ListBuffs _listBuffs;
        static void DrawImageAtLocation(Graphics g, Image image, Point location)
        {
            g.DrawImage(image, location);
        }
        [STAThread]
        static void Main()
        {

           /*CreateMainImage createMainImage = new CreateMainImage(CreateMainImage.Areas.Buff);
            createMainImage.CreateScreen();*/



            _listBuffs = new ListBuffs();
            _listBuffs.FullFillList();


            Load load = new Load();
            load.LoadData(_listBuffs);


            

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(_listBuffs));


            IconsPlace iconsPlace = new IconsPlace();
            iconsPlace.GeneratePlacePoints(32);

            








            save.SaveToFile(_listBuffs);








        }
        public static ListBuffs ReturnBuffs()
        {
            return _listBuffs;
        }

        public static void SaveIconData()
        {
            save.SaveToFile(_listBuffs);
        }
    }
}

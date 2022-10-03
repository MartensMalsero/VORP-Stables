using CitizenFX.Core;
using MenuAPI;
using System;

namespace VORP.Stables.Client.Menus
{
    class MainMenu
    {
        private static void SetupMenu(Menu mainMenu, dynamic user, string charJob)
        {
            MenuController.AddMenu(mainMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;

            #region Buy Horses Menu
            if (charJob != null)
            {
                if ((charJob == Convert.ToString(GetConfig.Config["JobForHorseDealer"]) || charJob == Convert.ToString(GetConfig.Config["JobForHorseAndCarriagesDealer"]))
                    && !Convert.ToBoolean(GetConfig.Config["DisableBuyOptions"]))
                {
                    int menuItemDesign = Convert.ToInt16(GetConfig.Config["MenuItemDesign"]);
                    switch (menuItemDesign)
                    {
                        case 0:
                            AddSubMenu(mainMenu, BuyHorsesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"], GetConfig.Langs["SubTitleMenuBuyHorses"]);
                            break;

                        case 1:
                            AddSubMenu(mainMenu, BuyHorsesMenu_MenuItem.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"], GetConfig.Langs["SubTitleMenuBuyHorses"]);
                            break;
                    }
                }
            }

            else
            {
                int menuItemDesign = Convert.ToInt16(GetConfig.Config["MenuItemDesign"]);
                switch (menuItemDesign)
                {
                    case 0:
                        AddSubMenu(mainMenu, BuyHorsesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"], GetConfig.Langs["SubTitleMenuBuyHorses"]);
                        break;

                    case 1:
                        AddSubMenu(mainMenu, BuyHorsesMenu_MenuItem.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"], GetConfig.Langs["SubTitleMenuBuyHorses"]);
                        break;
                }
            }
            #endregion

            #region My Horses Menu
            AddSubMenu(mainMenu, MyHorsesMenu.GetMenu(), GetConfig.Langs["TitleMenuHorses"], GetConfig.Langs["SubTitleMenuHorses"]);
            #endregion

            #region Buy Carriages Menu

            if (charJob != null)
            {
                if ((charJob == Convert.ToString(GetConfig.Config["JobForCarriagesDealer"]) || charJob == Convert.ToString(GetConfig.Config["JobForHorseAndCarriagesDealer"])) 
                    && !Convert.ToBoolean(GetConfig.Config["DisableBuyOptions"]))
                {
                    AddSubMenu(mainMenu, BuyCarriagesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyCarts"], GetConfig.Langs["SubTitleMenuBuyCarts"]);
                }
            }

            else
            {
                AddSubMenu(mainMenu, BuyCarriagesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyCarts"], GetConfig.Langs["SubTitleMenuBuyCarts"]);
            }
            #endregion

            #region My Carriages Menu
            AddSubMenu(mainMenu, MyCarriagesMenu.GetMenu(), GetConfig.Langs["TitleMenuCarts"], GetConfig.Langs["SubTitleMenuCarts"]);
            #endregion
            
            #region Stable wild horse
            if (charJob != null)
            {
                if (charJob == Convert.ToString(GetConfig.Config["JobForHorseDealer"]) ||
                    charJob == Convert.ToString(GetConfig.Config["JobForHorseAndCarriagesDealer"]))
                {
                    var stableMenuItem = new MenuItem(GetConfig.Langs["TitleStableWildHorse"], GetConfig.Langs["DescStableWildHorse"])
                    {
                        RightIcon = MenuItem.Icon.TICK
                    };
                    
                    mainMenu.AddMenuItem(stableMenuItem);
                    MenuController.BindMenuItem(mainMenu, mainMenu, stableMenuItem);
                }
            }
            
            else
            {
                var stableMenuItem = new MenuItem(GetConfig.Langs["TitleStableWildHorse"], GetConfig.Langs["DescStableWildHorse"])
                {
                    RightIcon = MenuItem.Icon.TICK
                };
                    
                mainMenu.AddMenuItem(stableMenuItem);
                MenuController.BindMenuItem(mainMenu, mainMenu, stableMenuItem);
            }
            #endregion

            #region OnItemSelect
            mainMenu.OnItemSelect += (_menu, _menuItem, _index) =>
            {
                Debug.WriteLine($"OnListItemSelect: [{_menu}, {_menuItem}, {_index}]");
                if (_index == 4)
                {
                    Debug.WriteLine("CALL StableWildHorse.cs");
                    //if player has a wild horse
                    //StableWildHorse
                }
            };
            #endregion
            
            #region OnMenuOpen
            mainMenu.OnMenuOpen += (_menu) =>
            {

            };
            #endregion

            #region OnMenuClose
            mainMenu.OnMenuClose += (_menu) =>
            {

            };
            #endregion

        }

        private static void AddSubMenu(Menu mainMenu, Menu GetMenu, string Title, string SubTitle)
        {
            MenuController.AddSubmenu(mainMenu, GetMenu);

            MenuItem subMenuBuyHorses = new MenuItem(Title, SubTitle)
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(subMenuBuyHorses);
            MenuController.BindMenuItem(mainMenu, GetMenu, subMenuBuyHorses);
        }

        public static Menu GetMenu(dynamic user, string CharJob)
        {
            Menu mainMenu = new Menu(GetConfig.Langs["TitleMenuStables"], GetConfig.Langs["SubTitleMenuStables"]);

            SetupMenu(mainMenu, user, CharJob);
            return mainMenu;
        }
    }
}

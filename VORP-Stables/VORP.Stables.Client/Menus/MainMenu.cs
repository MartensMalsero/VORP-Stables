using CitizenFX.Core;
using MenuAPI;
using System;
using CitizenFX.Core.Native;

namespace VORP.Stables.Client.Menus
{
    class MainMenu : BaseScript
    {
        private static void SetupMenu(Menu mainMenu, string charJob)
        {
            MenuController.AddMenu(mainMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control) 0;

            #region Buy Horses Menu

            if (charJob != null)
            {
                if ((charJob == Convert.ToString(GetConfig.Config["JobForHorseDealer"]) ||
                     charJob == Convert.ToString(GetConfig.Config["JobForHorseAndCarriagesDealer"]))
                    && !Convert.ToBoolean(GetConfig.Config["DisableBuyOptions"]))
                {
                    int menuItemDesign = Convert.ToInt16(GetConfig.Config["MenuItemDesign"]);
                    switch (menuItemDesign)
                    {
                        case 0:
                            AddSubMenu(mainMenu, BuyHorsesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"],
                                GetConfig.Langs["SubTitleMenuBuyHorses"], MenuItem.Icon.ARROW_RIGHT);
                            break;

                        case 1:
                            AddSubMenu(mainMenu, BuyHorsesMenu_MenuItem.GetMenu(),
                                GetConfig.Langs["TitleMenuBuyHorses"], GetConfig.Langs["SubTitleMenuBuyHorses"],
                                MenuItem.Icon.ARROW_RIGHT);
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
                        AddSubMenu(mainMenu, BuyHorsesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"],
                            GetConfig.Langs["SubTitleMenuBuyHorses"], MenuItem.Icon.ARROW_RIGHT);
                        break;

                    case 1:
                        AddSubMenu(mainMenu, BuyHorsesMenu_MenuItem.GetMenu(), GetConfig.Langs["TitleMenuBuyHorses"],
                            GetConfig.Langs["SubTitleMenuBuyHorses"], MenuItem.Icon.ARROW_RIGHT);
                        break;
                }
            }

            #endregion

            #region My Horses Menu

            AddSubMenu(mainMenu, MyHorsesMenu.GetMenu(), GetConfig.Langs["TitleMenuHorses"],
                GetConfig.Langs["SubTitleMenuHorses"], MenuItem.Icon.ARROW_RIGHT);

            #endregion

            #region Buy Carriages Menu

            if (charJob != null)
            {
                if ((charJob == Convert.ToString(GetConfig.Config["JobForCarriagesDealer"]) ||
                     charJob == Convert.ToString(GetConfig.Config["JobForHorseAndCarriagesDealer"]))
                    && !Convert.ToBoolean(GetConfig.Config["DisableBuyOptions"]))
                {
                    AddSubMenu(mainMenu, BuyCarriagesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyCarts"],
                        GetConfig.Langs["SubTitleMenuBuyCarts"], MenuItem.Icon.ARROW_RIGHT);
                }
            }

            else
            {
                AddSubMenu(mainMenu, BuyCarriagesMenu.GetMenu(), GetConfig.Langs["TitleMenuBuyCarts"],
                    GetConfig.Langs["SubTitleMenuBuyCarts"], MenuItem.Icon.ARROW_RIGHT);
            }

            #endregion

            #region My Carriages Menu

            AddSubMenu(mainMenu, MyCarriagesMenu.GetMenu(), GetConfig.Langs["TitleMenuCarts"],
                GetConfig.Langs["SubTitleMenuCarts"], MenuItem.Icon.ARROW_RIGHT);

            #endregion

            #region Stable wild horse
            
            var playerPed = API.GetPlayerPed(API.GetPlayerIndex());
            var horseEntity = API.GetMount(playerPed);

            if (!API.DoesEntityExist(horseEntity)) return;
            
            if (HorseManagment.MyHorses.Count >= int.Parse(GetConfig.Config["StableSlots"].ToString()))
            {
                TriggerEvent("vorp:TipRight", GetConfig.Langs["StableIsFullWildHorse"], 4000);
                return;
            }

            if (charJob != null)
            {
                if (charJob == Convert.ToString(GetConfig.Config["JobForHorseDealer"]) ||
                    charJob == Convert.ToString(GetConfig.Config["JobForHorseAndCarriagesDealer"]))
                {
                    AddSubMenu(mainMenu, StableWildHorse.GetMenu(),
                        GetConfig.Langs["TitleStableWildHorse"],
                        GetConfig.Langs["DescStableWildHorse"], MenuItem.Icon.TICK);
                }
            }

            else
            {
                AddSubMenu(mainMenu, StableWildHorse.GetMenu(),
                    GetConfig.Langs["TitleStableWildHorse"],
                    GetConfig.Langs["DescStableWildHorse"], MenuItem.Icon.TICK);
            }

            #endregion

            #region OnMenuOpen
            mainMenu.OnMenuOpen += (_menu) => { };
            #endregion

            #region OnMenuClose
            mainMenu.OnMenuClose += (_menu) => { };
            #endregion
        }

        private static void AddSubMenu(Menu mainMenu, Menu getMenu, string title, string subTitle, MenuItem.Icon icon)
        {
            MenuController.AddSubmenu(mainMenu, getMenu);

            var subMenuBuyHorses = new MenuItem(title, subTitle)
            {
                RightIcon = icon
            };

            mainMenu.AddMenuItem(subMenuBuyHorses);
            MenuController.BindMenuItem(mainMenu, getMenu, subMenuBuyHorses);
        }

        public static Menu GetMenu(string CharJob)
        {
            Menu mainMenu = new Menu(GetConfig.Langs["TitleMenuStables"], GetConfig.Langs["SubTitleMenuStables"]);

            SetupMenu(mainMenu, CharJob);
            return mainMenu;
        }
    }
}
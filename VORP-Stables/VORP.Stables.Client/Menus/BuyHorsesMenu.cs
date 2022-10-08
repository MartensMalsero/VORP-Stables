﻿using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VORP.Stables.Client.Menus
{
    class BuyHorsesMenu
    {
        private static Menu buyHorsesMenu =
            new Menu(GetConfig.Langs["TitleMenuBuyHorses"], GetConfig.Langs["SubTitleMenuBuyHorses"]);

        private static Menu subMenuConfirmBuy = new Menu(GetConfig.Langs["SubMenuConfirmBuy"], "");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;


            MenuController.AddMenu(buyHorsesMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control) 0;

            #region MenuConfirm

            MenuController.AddSubmenu(buyHorsesMenu, subMenuConfirmBuy);

            MenuItem buttonConfirmYes = new MenuItem("", GetConfig.Langs["ConfirmBuyButtonDesc"])
            {
                RightIcon = MenuItem.Icon.SADDLE
            };
            subMenuConfirmBuy.AddMenuItem(buttonConfirmYes);
            MenuItem buttonConfirmNo =
                new MenuItem(GetConfig.Langs["CancelBuyButton"], GetConfig.Langs["CancelBuyButtonDesc"])
                {
                    RightIcon = MenuItem.Icon.ARROW_LEFT
                };
            subMenuConfirmBuy.AddMenuItem(buttonConfirmNo);

            #endregion

            foreach (var cat in GetConfig.HorseLists)
            {
                List<string> hlist = new List<string>();
                List<string> pricing = new List<string>();
                int pricing_counter = 0;

                foreach (var h in cat.Value)
                {
                    hlist.Add(GetConfig.Langs[h.Key] + " - $" + Convert.ToString(h.Value));
                    pricing.Add(Convert.ToString(h.Value));
                }

                //To display the price in the description for this function, a major change in the MenuAPI is required.
                MenuListItem horseCategories = new MenuListItem(cat.Key, hlist, 0, "");

                buyHorsesMenu.AddMenuItem(horseCategories);
                MenuController.BindMenuItem(buyHorsesMenu, subMenuConfirmBuy, horseCategories);
            }

            //Events
            buyHorsesMenu.OnMenuOpen += (_menu) =>
            {
                StablesShop.BuyHorseMode();
                StablesShop.LoadHorsePreview(0, 0, StablesShop.HorsePed);
            };

            buyHorsesMenu.OnMenuClose += (_menu) => { StablesShop.ExitBuyHorseMode(); };

            subMenuConfirmBuy.OnItemSelect += async (_menu, _item, _index) =>
            {
                Debug.WriteLine($"OnItemSelect: [{_menu}, {_item}, {_index}]");

                if (_index == 0)
                {
                    StablesShop.ConfirmBuyHorse(GetConfig.Langs["InputNameButton"],
                        GetConfig.Langs["InputNamePlaceholder"], subMenuConfirmBuy.MenuTitle);
                }
                else
                {
                    subMenuConfirmBuy.CloseMenu();
                }
            };

            buyHorsesMenu.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                Debug.WriteLine($"OnListItemSelect: [{_menu}, {_listItem}, {_listIndex}, {_itemIndex}]");
                StablesShop.iIndex = _itemIndex;
                StablesShop.lIndex = _listIndex;
                subMenuConfirmBuy.MenuTitle = $"{GetConfig.HorseLists.ElementAt(_itemIndex).Key}";
                subMenuConfirmBuy.MenuSubtitle = string.Format(GetConfig.Langs["subTitleConfirmBuy"],
                    GetConfig.Langs[GetConfig.HorseLists.ElementAt(_itemIndex).Value.ElementAt(_listIndex).Key],
                    GetConfig.HorseLists.ElementAt(_itemIndex).Value.ElementAt(_listIndex).Value.ToString());
                buttonConfirmYes.Label = string.Format(GetConfig.Langs["ConfirmBuyButton"],
                    GetConfig.HorseLists.ElementAt(_itemIndex).Value.ElementAt(_listIndex).Value.ToString());

                StablesShop.horsecost = GetConfig.HorseLists.ElementAt(_itemIndex).Value.ElementAt(_listIndex).Value;
                StablesShop.horsemodel = GetConfig.HorseLists.ElementAt(_itemIndex).Value.ElementAt(_listIndex).Key;
            };

            buyHorsesMenu.OnIndexChange += async (_menu, _oldItem, _newItem, _oldIndex, _newIndex) =>
            {
                Debug.WriteLine($"OnIndexChange: [{_menu}, {_oldItem}, {_newItem}, {_oldIndex}, {_newIndex}]");
                MenuListItem itemlist = (MenuListItem) _newItem;
                Debug.WriteLine(itemlist.ListIndex.ToString());
                if (StablesShop.horseIsLoaded)
                {
                    await StablesShop.LoadHorsePreview(itemlist.Index, itemlist.ListIndex, StablesShop.HorsePed);
                }
            };

            buyHorsesMenu.OnListIndexChange += async (_menu, _listItem, _oldIndex, _newIndex, _itemIndex) =>
            {
                Debug.WriteLine($"OnListIndexChange: [{_menu}, {_listItem}, {_oldIndex}, {_newIndex}, {_itemIndex}]");
                if (StablesShop.horseIsLoaded)
                {
                    await StablesShop.LoadHorsePreview(_itemIndex, _newIndex, StablesShop.HorsePed);
                }
            };
        }


        public static Menu GetMenu()
        {
            SetupMenu();
            return buyHorsesMenu;
        }
    }
}
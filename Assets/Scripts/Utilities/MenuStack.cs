using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MenuStack : ScriptableObject
{
	private GameTime _gameTime;
	private Stack<MenuStackItem> _menus = new Stack<MenuStackItem>();
	
	private void Awake()
	{
		_gameTime = Resources.Load<GameTime>("GameTime");
		if (_gameTime == null)
			Debug.LogError("GameTime not found");
	}
	public void OpenMenu(GameObject menu)
	{
		OpenMenu(menu, true);
	}
	public void OpenMenu(GameObject menu, bool disableCurrent)
	{
		if (_menus.Any() && disableCurrent)
			_menus.Peek().MenuItem.SetActive(false);

		_menus.Push(new MenuStackItem(menu, disableCurrent, true));
		menu.SetActive(true);
		_gameTime.MenuPause = true;
	}
	public void OpenNonPausingMenu(GameObject menu)
	{
		OpenNonPausingMenu(menu, true);
	}
	public void OpenNonPausingMenu(GameObject menu, bool disableCurrent)
	{
		if (_menus.Any() && disableCurrent)
			_menus.Peek().MenuItem.SetActive(false);

		_menus.Push(new MenuStackItem(menu, disableCurrent, false));
		menu.SetActive(true);
	}
	public void CloseMenu(out int menusClosed)
	{
		menusClosed = 0;
		if (!_menus.Any())
			return;

		MenuStackItem menu;
		do
		{
			menu = _menus.Pop();
			menu.MenuItem.SetActive(false);
			menusClosed++;
			if (!_menus.Any(m=>m.Pausing))
			{
				_gameTime.MenuPause = false;
			}
			else
			{
				_menus.Peek().MenuItem.SetActive(true);
			}
		} while (!menu.Independent);
	}
	public void CloseMenu()
	{
		CloseMenu(out _);
	}
	private class MenuStackItem
	{
		public GameObject MenuItem;
		public bool Independent;
		public bool Pausing;
		public MenuStackItem(GameObject menuItem, bool independent, bool pausing)
		{
			MenuItem = menuItem;
			Independent = independent;
			Pausing = pausing;
		}
	}
}
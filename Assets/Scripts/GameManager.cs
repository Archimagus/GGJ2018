using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]private Transform _playerStartLocation;
	public Transform PlayerStartLocation
	{
		get
		{
			if(_playerStartLocation == null)
				_playerStartLocation = GameObject.Find("PlayerStart").transform;
			return _playerStartLocation;
		}
	}
	private GameTime _gameTime;
	private MenuStack _menuStack;
	public static GameManager Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;

		DontDestroyOnLoad(gameObject);

		_gameTime = Resources.Load<GameTime>("GameTime");
		if (_gameTime == null)
			Debug.LogError("GameTime not found");

		_menuStack = Resources.Load<MenuStack>("MenuStack");
		if (_menuStack == null)
			Debug.LogError("MenuStack not found");
	}
	
	private void Update()
	{
		if (Input.GetButtonDown("Menu"))
		{
			if (_gameTime.MenuPause)
				_menuStack.CloseMenu();
			else
				_menuStack.OpenMenu(PauseMenu.Instance.PausePanel);
		}
	}
	public void QuitToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	private void OnLevelWasLoaded(int level)
	{
		if(level == 0)
			Destroy(gameObject);
	}
}
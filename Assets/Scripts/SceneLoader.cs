using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public SceneField SceneToLoad;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			SceneManager.LoadScene(SceneToLoad.SceneName);
		}
	}
}

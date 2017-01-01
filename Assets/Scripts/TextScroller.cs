using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextScroller : MonoBehaviour {
  public float speed;

  private Text text;
  private Scrollbar vertBar;
  private bool scrolling;
  private void Awake()
  {
    text = transform.Find("Viewport/Text").GetComponent<Text>();
    vertBar = transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
    scrolling = false;
  }

  void Start () {
    StartScrolling();
    LoadTextAsset("quijote_short");
	}
	
	// Update is called once per frame
	void Update () {
    if (!scrolling)
      return;
    vertBar.value = Mathf.Clamp( vertBar.value - (Time.deltaTime*speed), 0, 1);
    if (vertBar.value >= 1)
      scrolling = false;
	}

  public void StartScrolling()
  {
    StartCoroutine( SetVarValue(1) );
    scrolling = true;
  }

  /**
   * Setting the value of the bar will not work if no frame has been
   * rendered yet since it will be overwritten.
   * So we set it, yield 0 (which waits 1 frame) and set it again to
   * ensure it is set
   */
  IEnumerator SetVarValue(float v)
  {
    vertBar.value = v;
    yield return 0;
    vertBar.value = v;
  }

  public void LoadTextAsset( string resourceName )
  {
    TextAsset textAsset = Resources.Load(resourceName, typeof(TextAsset)) as TextAsset;
    text.text = textAsset.text;
  }
}

using UnityEngine;
using System.Collections;
using UnityEditor;

public class Test : Editor
{
	[MenuItem("Bunlde Editor/Create One By One")]
	static void CreateAssetBunldesMain ()
	{
        // 取得所有選取對象.
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
 
        //遍历所有的游戏对象
		foreach (Object obj in SelectedAsset) 
		{
			//本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
			//StreamingAssets是只读路径，不能写入
			//服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android)) {
  				Debug.Log(obj.name +"资源打包成功");
			} 
			else 
 				Debug.Log(obj.name +"资源打包失败");
		}
		//刷新编辑器
		AssetDatabase.Refresh();
		
	}
	
	[MenuItem("Bunlde Editor/Create ALL In One")]
	static void CreateAssetBunldesALL ()
	{

		Caching.CleanCache (); 

		string Path = Application.dataPath + "/StreamingAssets/Story.assetbundle"; 
		
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
 
		foreach (Object obj in SelectedAsset) 
			Debug.Log ("Create AssetBunldes name :" + obj);

		//这里注意第二个参数就行
		if (BuildPipeline.BuildAssetBundle (null, SelectedAsset, Path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android)) {
			AssetDatabase.Refresh ();
		}
	}

    [MenuItem("Bunlde Editor/Replace Sprite")]
    static void Replace2DSprite()
    {
        // 取得所有選取對象.
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            GameObject pGObj = (GameObject)obj;

            UI2DSprite[] p2DSprite= pGObj.GetComponentsInChildren<UI2DSprite>();
            Material pMaterial = Resources.Load("Diffuse") as Material;
            Debug.Log(pGObj.name + " Length:" + p2DSprite.Length);

            foreach (UI2DSprite pSprite in p2DSprite)
            {
                GameObject pS_Obj = pSprite.gameObject;
                SpriteRenderer pRenderer = pS_Obj.AddComponent<SpriteRenderer>();
                pRenderer.sprite = pSprite.sprite2D;
                pRenderer.color = pSprite.color;
                pRenderer.sortingLayerName = "Enemy";
                pRenderer.sortingOrder = pSprite.depth;
                pRenderer.material = pMaterial;

                pS_Obj.transform.localScale = new Vector3(100 * pSprite.gameObject.transform.localScale.x, 100 * pSprite.gameObject.transform.localScale.y, 1);
                DestroyImmediate(pSprite);
            }            
        }
        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("Bunlde Editor/Replace Sorting Layer")]
    static void ReplaceSortingLayer()
    {
        // 取得所有選取對象.
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            GameObject pGObj = (GameObject)obj;           

            SpriteRenderer[] pRenderer = pGObj.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pSprite in pRenderer)
            {
                pSprite.sortingLayerName = "Item";
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("Bunlde Editor/Replace To 2D Sprite")]
    static void ReplaceTo2DSprite()
    {
        // 取得所有選取對象.
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            GameObject pGObj = (GameObject)obj;

            SpriteRenderer[] pRenderer = pGObj.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pRender in pRenderer)
            {
                pRender.gameObject.transform.localScale = Vector3.one;
                UI2DSprite pSprite = pRender.gameObject.AddComponent<UI2DSprite>();
                pSprite.sprite2D = pRender.sprite;
                pSprite.depth = pRender.sortingOrder;
                pSprite.color = pRender.color;
                pSprite.MakePixelPerfect();
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("Bunlde Editor/Replace To Sprite")]
    static void ReplaceToSprite()
    {
        // 取得所有選取對象.
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            GameObject pGObj = (GameObject)obj;

            SpriteRenderer[] pRenderer = pGObj.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pRender in pRenderer)
                pRender.material = Resources.Load("Sprite") as Material;
        }
        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("Sprite Tool/UGUI To NGUI")]
    static void UGUIToNGUI()
    {
        // 取得所有選取對象.
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            GameObject pGObj = (GameObject)obj;

            SpriteRenderer[] pRenderer = pGObj.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pRender in pRenderer)
            {
                pRender.gameObject.transform.localScale = Vector3.one;
                UI2DSprite pSprite = pRender.gameObject.AddComponent<UI2DSprite>();
                pSprite.sprite2D = pRender.sprite;
                pSprite.depth = pRender.sortingOrder;
                pSprite.color = pRender.color;
                pSprite.MakePixelPerfect();
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();
    }
}

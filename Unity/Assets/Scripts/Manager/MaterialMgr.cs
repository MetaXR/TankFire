using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using UnityEditor;

public class MaterialMgr {

	[MenuItem ("Example/Select All of Tag...")]
	public static void SelectAllOfTagWizard() 
	{
		Material mat = new Material (Shader.Find("Standard"));

		mat.color = new Color(1f, 0f, 0f, 0.5f);
		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		mat.SetInt("_ZWrite", 0);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.DisableKeyword("_ALPHABLEND_ON");
		mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = 3000;

		for (int i = 0; i < Selection.objects.Length; i++) {
			GameObject go = (GameObject)Selection.objects [i];
			MeshRenderer mr = go.GetComponent<MeshRenderer> ();
			if (mr != null) {

				Material[] mats = new Material[mr.sharedMaterials.Length];
				for (int j = 0; j < mr.sharedMaterials.Length; j++) {
					mats [j] = mr.sharedMaterials [j];
				}
				mats [mr.sharedMaterials.Length-1] = mat;
				mr.materials = mats;
			}
		}
	}
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileMgr {
	/*
	public static Texture2D LoadTexture(string filepath){
		Texture2D texture = new Texture2D(100, 100);
		byte[] bytes = FileManager.LoadData (filepath);
		texture.LoadImage(bytes);
		return texture;
	}

	public static MovieTexture LoadMovieTexture(string filepath){
		MovieTexture texture = new MovieTexture(1, 1);
		byte[] bytes = FileManager.LoadData (filepath);
        texture.LoadImage(bytes);
		return texture;
	}
	
	public static byte[] LoadData(string filepath)
	{
        FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        byte[] bytes = new byte[fileStream.Length]; 

        fileStream.Read(bytes, 0, (int)fileStream.Length);
        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;
        return bytes
	}
	*/
}

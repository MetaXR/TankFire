using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Reflection;

namespace ExcelParser
{
    public class DataMgrBase<T> : MonoBehaviour,IDataMgrBase where T : MonoBehaviour
	{
		private Dictionary<object,IDataBean> m_idDataDic = new Dictionary<object, IDataBean> ();
		private List<object> m_idList = new List<object> ();
		private bool m_isInit = false;

		/// <summary>
		/// Inits the data.
		/// </summary>
        public void InitData ()
		{
			if(m_isInit)
            {
                return;   
            }

			Type dataBeanType = GetBeanType ();

			TextAsset txt = Resources.Load (GetXlsxPath ()) as TextAsset;

			string dataTxt = txt.ToString ();

			dataTxt = dataTxt.Replace ("\r", "");
//			dataTxt = dataTxt.Replace (" ", "");
//			dataTxt = dataTxt.Replace (" ", "");
			string[] hList = dataTxt.Split ('\n');
			
			
			string title = hList [2];
			string[] titles = title.Split ('\t');
			string[] types = hList [0].Split ('\t');



			for (int col = 3; col < hList.Length; col++) {
				IDataBean dataBean = null;
				object key = null;

				string[] vals = hList [col].Split ('\t');

				if (vals.Length != titles.Length) {
					continue;
				}

				dataBean = (IDataBean)Activator.CreateInstance (dataBeanType);
				for (int row = 0; row < titles.Length; row++) {

					string titleName = titles [row];

					if (string.IsNullOrEmpty (titleName)) {
						continue;
					}

					string typeStr = types [row];
					string valStr = vals [row];

					if (string.IsNullOrEmpty (typeStr)) {
						continue;
					}

					string propertyName = titleName.Substring (0, 1).ToUpper () + titleName.Substring (1);

					PropertyInfo prop = dataBeanType.GetProperty (propertyName, BindingFlags.Public | BindingFlags.Instance);

					object val = Convert.ChangeType (valStr, prop.PropertyType);

					prop.SetValue (dataBean, val, null);

					//set dictionary id
					if (row == 0) {
						key = val;
					}

				}

				if (dataBean != null) {
					m_idDataDic.Add (key, dataBean);
					m_idList.Add (key);
				}
			}

			m_isInit = true;
           

		}


		/// <summary>
		/// Gets the xlsx txt path. Need overwrite.
		/// </summary>
		/// <returns>The xlsx path.</returns>
		protected virtual string GetXlsxPath ()
		{
			return "";
		}


		/// <summary>
		/// Gets the type of the bean.Need overwrite.
		/// </summary>
		/// <returns>The bean type.</returns>
		protected virtual Type GetBeanType ()
		{
			return null;
		}

		public IDataBean _GetDataById (object id)
		{
			if (m_idDataDic.ContainsKey (id)) {
				return m_idDataDic [id];
			} else {
				return null;
			}
		}

		public IDataBean _GetDataByRow(int row)
		{
			if (row >= 0 && row < m_idList.Count) {
				object id = m_idList [row];
				return _GetDataById (id);
			} else {
				return null;
			}
		}
	    public int count{ get{ return m_idList.Count;}}

	}
}

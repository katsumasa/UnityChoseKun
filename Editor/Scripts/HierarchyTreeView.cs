using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// Hierarchyを表示する為のClass
    /// Programed by Katsumasa.Kimura
    /// https://docs.unity3d.com/ja/2019.4/Manual/TreeViewAPI.html
    /// https://docs.unity3d.com/ja/2019.4/ScriptReference/IMGUI.Controls.TreeView.html
    /// </summary>
    public class HierarchyTreeView : TreeView
    {        
        public delegate void SelectionChangedCB(IList<int> selectedIds);


        static  class Styles {
            public static Texture2D gameObjectIcon = (Texture2D)EditorGUIUtility.Load("d_GameObject Icon");
            public static Texture2D prefabIcon = (Texture2D)EditorGUIUtility.Load("d_Prefab Icon");
#if UNITY_2019_1_OR_NEWER
            public static Texture2D sceneAssetIcon = (Texture2D)EditorGUIUtility.Load("d_SceneAsset Icon");
#else
            public static Texture2D sceneAssetIcon = (Texture2D)EditorGUIUtility.Load("SceneAsset Icon");
#endif
        }

        
        [SerializeField] SceneKun m_sceneKun;
        [SerializeField] SelectionChangedCB m_selectionChangeCB;


        public SceneKun sceneKun {
            get {if(m_sceneKun == null){m_sceneKun = new SceneKun();}return m_sceneKun;}
            set {m_sceneKun = value;}
        }
        

        public SelectionChangedCB selectionChangeCB {
            get {return m_selectionChangeCB;}
            set {m_selectionChangeCB = value;}
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="state"></param>
        public HierarchyTreeView(TreeViewState state) : base(state){}


                                
        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(id:-1,depth:-1,displayName:"Root");                        
            root.children = new List<TreeViewItem>();                            
            if(sceneKun != null && sceneKun.gameObjectKuns != null){                
                var scene = new TreeViewItem(id:0,depth:0,displayName:sceneKun.name);
                scene.children = new List<TreeViewItem>();
                root.children.Add(scene);                            
                var parents =  sceneKun.gameObjectKuns.Where((q)=>q.transformKun.parentInstanceID == 0);
                if(parents != null && parents.Count()!=0){
                    foreach(var parent in parents)
                    {
                        scene.children.Add(MakeTreeViewRecursive(parent,1));
                    }
                }
            }
            return root;
        }


        protected override void RowGUI (RowGUIArgs args)
        {
            Texture2D icon;

            var colorBackUp = GUI.color;
            if(sceneKun.gameObjectKuns != null){            
                var gameObjectKun = sceneKun.gameObjectKuns.Where((g)=>g.instanceID == args.item.id).FirstOrDefault();
                if(gameObjectKun != null){
                    if(gameObjectKun.activeSelf){
                        GUI.color = Color.white;                
                    } else {
                        GUI.color = Color.gray;
                    }
                }
            }
            
            

            if(args.item.id == 0){
                icon = Styles.sceneAssetIcon;
            } else {
                icon = Styles.gameObjectIcon;
            }
            var toggleRect = args.rowRect;
            toggleRect.x += GetContentIndent(args.item);
            toggleRect.width = 16f;
            GUI.DrawTexture(toggleRect,icon);
            extraSpaceBeforeIconAndLabel = toggleRect.width + 2f;            
            base.RowGUI(args);            
            GUI.color = colorBackUp;
        }


        TreeViewItem MakeTreeViewRecursive(GameObjectKun go,int depth)
        {
            var treeViewItem = new TreeViewItem(id:go.instanceID,depth:depth,displayName:go.name);            
            var children = sceneKun.gameObjectKuns.Where((q)=>q.transformKun.parentInstanceID == go.transformKun.instanceID);
            if(children != null && children.Count() != 0){
                treeViewItem.children = new List<TreeViewItem>();
                foreach(var child in children){
                    treeViewItem.children.Add(MakeTreeViewRecursive(child,depth+1));
                }
            }            
            return treeViewItem;
        }

        protected override void SelectionChanged (IList<int> selectedIds)
        {
            base.SelectionChanged(selectedIds);            
            if (m_selectionChangeCB != null){
                m_selectionChangeCB(selectedIds);
            }
        }
    }
}
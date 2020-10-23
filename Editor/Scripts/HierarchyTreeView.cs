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


        static class Styles
        {
            public static Texture2D gameObjectIcon = (Texture2D)EditorGUIUtility.Load("d_GameObject Icon");
            public static Texture2D prefabIcon = (Texture2D)EditorGUIUtility.Load("d_Prefab Icon");
#if UNITY_2019_1_OR_NEWER
            public static Texture2D sceneAssetIcon = (Texture2D)EditorGUIUtility.Load("d_SceneAsset Icon");
#else
            public static Texture2D sceneAssetIcon  = (Texture2D)EditorGUIUtility.Load("SceneAsset Icon");
#endif
        }


        [SerializeField] SceneKun m_sceneKun;
        [SerializeField] SelectionChangedCB m_selectionChangeCB;

        public SceneKun sceneKun
        {
            get { if (m_sceneKun == null) { m_sceneKun = new SceneKun(); } return m_sceneKun; }
            set { m_sceneKun = value; }
        }

        public SelectionChangedCB selectionChangeCB
        {
            get { return m_selectionChangeCB; }
            set { m_selectionChangeCB = value; }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="state"></param>
        public HierarchyTreeView(TreeViewState state) : base(state) { }



        protected override TreeViewItem BuildRoot()
        {                 

            var root = new TreeViewItem(id: -1, depth: -1, displayName: "Root");
            root.children = new List<TreeViewItem>();
            if (sceneKun != null && sceneKun.gameObjectKuns != null) {
                // Scene
                var scene = new TreeViewItem(id: 0, depth: 0, displayName: sceneKun.name);
                root.AddChild(scene);
                                
                // Sceneの直下に来るGameObjectとその子になるGameObjectを再帰的に追加
                var parents = sceneKun.gameObjectKuns.Where((q) => q.transformKun.parentInstanceID == 0);

                if (parents != null && parents.Count() != 0) {
                    foreach (var parent in parents)
                    {
                        var treeViewItem = MakeTreeViewRecursive(parent, 1);
                        scene.AddChild(treeViewItem);
                        
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
                    var tvi = MakeTreeViewRecursive(child, depth + 1);
                    tvi.parent = treeViewItem;
                    treeViewItem.children.Add(tvi);
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


        const string k_GenericDragID = "GenericDragColumnDragging";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">Drag&Dropの対象となるTreeViewItemの情報が含まれる</param>
        /// <returns></returns>
        protected override bool CanStartDrag(CanStartDragArgs args)
        {
            Debug.Log("CanStartDrag");
            base.CanStartDrag(args);
            return true;
        }


        protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
        {
            Debug.Log("SetupDragAndDrop");
            base.SetupDragAndDrop(args);


            DragAndDrop.PrepareStartDrag();
            var draggedRows = GetRows().Where(item => args.draggedItemIDs.Contains(item.id)).ToList();
            DragAndDrop.SetGenericData(k_GenericDragID, draggedRows);
            DragAndDrop.objectReferences = new UnityEngine.Object[] { }; // this IS required for dragging to work
            string title = draggedRows.Count == 1 ? draggedRows[0].displayName : "< Multiple >";
            DragAndDrop.StartDrag(title);
        }


        protected override DragAndDropVisualMode HandleDragAndDrop (DragAndDropArgs args)
        {
            Debug.Log("HandleDragAndDrop: " + args.dragAndDropPosition);


            base.HandleDragAndDrop(args);


            // Check if we can handle the current drag data (could be dragged in from other areas/windows in the editor)
            var draggedRows = DragAndDrop.GetGenericData(k_GenericDragID) as List<TreeViewItem>;
            if (draggedRows == null)
                return DragAndDropVisualMode.None;

            // Parent item is null when dragging outside any tree view items.
            switch (args.dragAndDropPosition)
            {
                case DragAndDropPosition.UponItem:
                case DragAndDropPosition.BetweenItems:
                    {
                        bool validDrag = ValidDrag(args.parentItem, draggedRows);
                        if (args.performDrop && validDrag)                            
                        {
                            Debug.Log("Drop");
                            MoveTreeViewItem(draggedRows[0], args.parentItem, args.insertAtIndex);                                                        
                        }
                        return validDrag ? DragAndDropVisualMode.Move : DragAndDropVisualMode.None;
                    }
                    
                case DragAndDropPosition.OutsideItems:
                    {
                        if (args.performDrop)
                        {
                            Debug.Log("Drop");
                            //OnDropDraggedElementsAtIndex(draggedRows, m_TreeModel.root, m_TreeModel.root.children.Count);
                        }
                        return DragAndDropVisualMode.Move;
                    }
                    
                default:
                    Debug.LogError("Unhandled enum " + args.dragAndDropPosition);
                    return DragAndDropVisualMode.None;

            }
        }


        bool ValidDrag(TreeViewItem parent, List<TreeViewItem> draggedItems)
        {
            TreeViewItem currentParent = parent;
            while (currentParent != null)
            {
                if (draggedItems.Contains(currentParent))
                {
                    return false;
                }
                currentParent = currentParent.parent;
            }
            return true;
        }


        void MoveTreeViewItem(TreeViewItem treeViewItem,TreeViewItem parent,int insertIndex)
        {

            // 親が同じな場合は
            if (treeViewItem.parent == parent)
            {
                insertIndex--;
            }
            if (treeViewItem.parent != null)
            {
                treeViewItem.parent.children.Remove(treeViewItem);                
                treeViewItem.parent = null;
            }

            if(parent.children == null)
            {                
                parent.AddChild(treeViewItem);
            } 
            else
            {
                parent.children.Insert(insertIndex == -1 ? 0 : insertIndex,treeViewItem);
            }

            treeViewItem.parent = parent;
            UpdateDepth(treeViewItem);


            var gameObjectKun = sceneKun.gameObjectKuns.Where((g) => g.instanceID == treeViewItem.id).FirstOrDefault();
            
            if (parent != null)
            {
                var parentKun = sceneKun.gameObjectKuns.Where((g) => g.instanceID == parent.id).FirstOrDefault();
                if (parentKun == null)
                {
                    gameObjectKun.transformKun.parentInstanceID = 0;
                }
                else
                {
                    gameObjectKun.transformKun.parentInstanceID = parentKun.transformKun.instanceID;
                }
            } 
            else
            {
                gameObjectKun.transformKun.parentInstanceID = 0;
            }
            Reload();            
        }


        void UpdateDepth(TreeViewItem treeViewItem)
        {
            treeViewItem.depth = treeViewItem.parent.depth + 1;
            if (treeViewItem.children != null)
            {
                foreach (var chield in treeViewItem.children)
                {
                    UpdateDepth(chield);
                }
            }
        }
    
    }
}
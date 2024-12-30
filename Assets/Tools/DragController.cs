using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public RectTransform canvas;
    // 储存物品最放的最近一个物品栏
    public RectTransform lastSlot;

    // 这个参数用来调整鼠标点击时，鼠标坐标与物品坐标的偏移量
    private Vector3 dragOffset;
    private CanvasGroup canvasGroup;

    public BackPack backPack;

    private void Start()
    {
        lastSlot = transform.parent as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();  
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //transform.SetParent(canvas);
        canvasGroup.blocksRaycasts = false;
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, eventData.position, null, out worldPos))
        {
            dragOffset = new Vector3(transform.position.x - worldPos.x, transform.position.y - worldPos.y, 0f);
            transform.position = worldPos + dragOffset;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, eventData.position, null, out worldPos))
        {
            transform.position = worldPos + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 没有对齐格子时，返回原来的背包格子
        if (eventData.pointerEnter == null || eventData.pointerEnter.tag != "Slot")
        {
            PutItem(lastSlot);
        }
    }

    // 将物品放入一个物品栏内
    public void PutItem(RectTransform slot)
    {
        backPack.SwitchBackPackItem(int.Parse(slot.name), int.Parse(lastSlot.name));
        //Debug.Log(lastSlot.name);
        lastSlot = slot;
        transform.SetParent(slot);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        //Debug.Log(lastSlot.name);

       //int slotIndex = GetSlotIndex(slot); // 取得該插槽的索引
        //backPack.UpdatePrefabForSlot(int.Parse(lastSlot.name), itemPrefab);  // 更新 Prefab
    }

    // 当有其他物品想要放在自己这格时，双方交换一下位置
    public void OnDrop(PointerEventData eventData)
    {
        // 先让物品栏高亮效果消失
        lastSlot.GetComponent<DropController>().HideColor();
        var dc = eventData.pointerDrag.GetComponent<DragController>();
        var tempSlot = dc.lastSlot;
        dc.PutItem(lastSlot);
        PutItem(tempSlot);
    }

     private int GetSlotIndex(RectTransform slot)
    {
        for (int i = 0; i < backPack.slots.Length; i++)
        {
            if (backPack.slots[i] == slot.gameObject)
            {
                return i;  // 返回匹配的索引
            }
        }

        return -1;  // 沒有找到匹配的插槽
    }
}

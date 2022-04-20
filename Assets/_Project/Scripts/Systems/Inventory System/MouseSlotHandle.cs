using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ACoolTeam
{
    public class MouseSlotHandle : MonoBehaviour
    {
        #region Fields / Properties
        public static InventorySlot MouseSlot { get; set; }

        [SerializeField] private GameObject _dropIndicator;
        [SerializeField] private Camera _playerCam;
        [SerializeField] private GridLayout _gridLayout;
        [SerializeField] private InventoryObject _playerInventory;
        [SerializeField] private UnityEvent _onHandDrop;

        private static GameObject _dropIndicRef;
        private Image _itemImage;
        private TextMeshProUGUI _itemAmountText;
        private PlayerInput _playerInput;
        #endregion

        #region Unity Methods / Startup
        private void Awake()
        {
            MouseSlot = new InventorySlot();
            _playerInput = new PlayerInput();
            _itemImage = transform.GetChild(0).GetComponent<Image>();
            _itemAmountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
        private void Start()
        {
            _dropIndicRef = Instantiate(_dropIndicator, new Vector2(0, 0), Quaternion.identity);
            _dropIndicRef.SetActive(false);
            _playerInput.Player.Fire.started += DropLeftHandle;
            //_playerInput.Player.RightClick.started += DropRightHandle;
        }

        private void Update()
        {
            //UpdateMouseSlot();
            //UpdateDropIndicator();
        }
        #endregion

        #region Drop Logic
        private void DropLeftHandle(InputAction.CallbackContext context)
        {
            //var selectedSlot = HotbarHandle.SelectedSlot;
            //if (MouseSlot.HasItem() && GlobalHandle.PointNotOverUIExcMouse())
            //    StartCoroutine(SpawnItemFromMouseCo());
            //else if (selectedSlot.SlotItem.ItemType == ItemType.Deployable && GlobalHandle.PointNotOverUIExcMouse())
            //    StartCoroutine(SpawnItemFromHandCo());
        }

        private void DropRightHandle(InputAction.CallbackContext context)
        {
            //var selectedSlot = HotbarHandle.SelectedSlot;
            //if (MouseSlot.HasItem() && GlobalHandle.PointNotOverUIExcMouse())
            //{
            //    DropOneItem(MouseSlot);
            //}
            //else if ((selectedSlot.SlotItem.ItemType == ItemType.Resource || selectedSlot.SlotItem.ItemType == ItemType.Deployable) && GlobalHandle.PointNotOverUI())
            //{
            //    DropOneItem(selectedSlot);
            //}
        }

        //private IEnumerator SpawnItemFromHandCo()
        //{
        //    yield return null;
        //    var spawnPos = _dropIndicRef.transform.position;
        //    var selectedSlot = HotbarHandle.SelectedSlot;

        //    if (GridSystem.current.CanInstItem(spawnPos, 6) && GridSystem.current.CanInstItem(spawnPos, 3))
        //    {
        //        Instantiate(selectedSlot.SlotItem.DeployPrefab, spawnPos, Quaternion.identity);
        //        selectedSlot.CurrentStack--;
        //        if (selectedSlot.CurrentStack == 0)
        //            selectedSlot.OverrideSlot(-1, new Item(), 0);
        //        _onHandDrop?.Invoke();
        //    }
        //}

        //private IEnumerator SpawnItemFromMouseCo()
        //{
        //    yield return null;
        //    var spawnPos = _dropIndicRef.transform.position;
        //    if (GridSystem.current.CanInstItem(spawnPos, 3))
        //    {
        //        if (MouseSlot.SlotItem.ItemType == ItemType.Deployable)
        //        {
        //            if (GridSystem.current.CanInstItem(spawnPos, 6))
        //            {
        //                Instantiate(MouseSlot.SlotItem.DeployPrefab, spawnPos, Quaternion.identity);
        //                MouseSlot.CurrentStack--;
        //                if (MouseSlot.CurrentStack == 0)
        //                    MouseSlot.OverrideSlot(-1, new Item(), 0);
        //            }
        //        }
        //        else
        //        {
        //            var item = Instantiate(MouseSlot.SlotItem.ItemPrefab, spawnPos, Quaternion.identity);
        //            if (item.TryGetComponent(out ItemWorldBehavior iwb)) iwb.Amount = MouseSlot.CurrentStack;
        //            MouseSlot = new InventorySlot();
        //        }

        //        _onHandDrop?.Invoke();
        //    }
        //}

        //private void DropOneItem(InventorySlot slot)
        //{
        //    var spawnPos = _dropIndicRef.transform.position;

        //    if (GridSystem.current.CanInstItem(spawnPos, 3))
        //    {
        //        var item = Instantiate(slot.SlotItem.ItemPrefab, spawnPos, Quaternion.identity);
        //        if (item.TryGetComponent(out ItemWorldBehavior iwb)) iwb.Amount = 1;
        //        slot.CurrentStack--;
        //        if (slot.CurrentStack == 0)
        //            slot.OverrideSlot(-1, new Item(), 0);
        //        _onHandDrop?.Invoke();
        //    }
        //}
        #endregion

        #region Logic Update Function Methods
        //private void UpdateMouseSlot()
        //{
        //    if (MouseSlot.HasItem())
        //        SetInfo(true);
        //    else
        //        SetInfo(false);
        //}

        //private void SetInfo(bool val)
        //{
        //    if (val)
        //    {
        //        transform.position = Mouse.current.position.ReadValue();
        //        _itemImage.sprite = MouseSlot.SlotItem.UiDisplay;
        //        _itemAmountText.text = MouseSlot.CurrentStack.ToString();
        //    }
        //    else
        //    {
        //        transform.position = new Vector2(0, -700);
        //    }
        //}

        //private void UpdateDropIndicator()
        //{
        //    var type = HotbarHandle.SelectedSlot.SlotItem.ItemType;
        //    if (MouseSlot.HasItem() || type == ItemType.Resource || type == ItemType.Deployable)
        //    {
        //        _dropIndicRef.SetActive(true);
        //        var mouseWorldPos = (Vector2)_playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //        var playerPos = (Vector2)transform.root.transform.position + new Vector2(0, 0.5f);

        //        if (Vector2.Distance(playerPos, mouseWorldPos) > 1.5f)
        //        {
        //            var dist = mouseWorldPos - playerPos;
        //            dist.Normalize();
        //            dist *= 1.5f;
        //            dist += playerPos;
        //            _dropIndicRef.transform.position = _gridLayout.WorldToCell(dist);
        //        }
        //        else
        //        {
        //            _dropIndicRef.transform.position = _gridLayout.WorldToCell(mouseWorldPos);
        //        }
        //    }
        //    else
        //    {
        //        _dropIndicRef.SetActive(false);
        //    }
        //}
        #endregion

    }
}

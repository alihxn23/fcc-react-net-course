
const Item = ({ item, drag, itemImgObj }) => {
    console.log('from item ', item, itemImgObj)
    return (
        <div className="unranked-cell">
            <img id={`item-${item.id}`} src={itemImgObj.image}
                style={{ cursor: "pointer" }} draggable="true" onDragStart={drag}
            />
        </div>
    )
}
export default Item;
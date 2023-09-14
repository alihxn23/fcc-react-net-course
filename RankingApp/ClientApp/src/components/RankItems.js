import React, { useState, useEffect } from 'react';
import MovieImageArr from './MovieImages';
import RankingGrid from './RankingGrid';

const RankItems = () => {
    const [items, setItems] = useState([]);

    const drag = (ev) => {
        console.log('from drag')
        ev.dataTransfer.setData("text", ev.target.id);
    }

    const allowDrop = (ev) => {
        console.log('from allowDrop')
        ev.preventDefault();
    }

    const drop = (ev) => {
        console.log('from drop')
        ev.preventDefault();
        const targetElm = ev.target;
        if (targetElm.nodeName === "IMG") {
            return false;
        }
        if (targetElm.childNodes.length === 0) {
            var data = parseInt(ev.dataTransfer.getData("text").substring(5));
            const transformedCollection = items.map((item) => (item.id === parseInt(data)) ?
                { ...item, ranking: parseInt(targetElm.id.substring(5)) } : { ...item, ranking: item.ranking });
            setItems(transformedCollection);
        }

    }

    const dataType = 1;

    useEffect(() => {
        fetch(`item/${dataType}`)
            .then(results => results.json())
            .then(jsonResult => setItems(jsonResult))
    }, [])

    return (
        <main>
            <RankingGrid items={items} imgArr={MovieImageArr} drag={drag} allowDrop={allowDrop} drop={drop} />
            <div className="items-not-ranked">
                {(items.length > 0) ? items.map(item =>
                    <div className="unranked-cell">
                        <img
                            id={`item-${item.id}`}
                            src={MovieImageArr.find(m => m.id === item.imageId)?.image}
                            style={{ cursor: "pointer" }}
                            draggable="true"
                            onDragStart={drag}>
                        </img>
                    </div>
                ) : <div>loading ...</div>}
            </div>
        </main>
    )
}

export default RankItems;
import React, { useState, useEffect } from 'react';
import MovieImageArr from './MovieImages';
import RankingGrid from './RankingGrid';

const RankItems = () => {
    const [items, setItems] = useState([]);

    const drag = (ev) => {
        ev.dataTransfer.setData("text", ev.target.id);
    }

    const allowDrop = (ev) => { ev.preventDefault() }

    const drop = (ev) => {
        ev.preventDefault();
        const targetElem = ev.target;
        if (targetElem.nodeName === 'IMG') {
            return false
        }
        if (targetElem.childNodes.length === 0) {
            let data = parseInt(ev.dataTransfer.getData("text").subString(5))
            const transformedCollection = items.map(item =>
                item.id === parseInt(data) ?
                    { ...item, ranking: parseInt(targetElem.id.subString(5)) }
                    :
                    { ...item, ranking: item.ranking }
            )
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
            <RankingGrid items={items} imgArr={MovieImageArr} />
            <div className="items-not-ranked">
                {(items.length > 0) ? items.map(item =>
                    <div className="unranked-cell">
                        <img id={`item-${item.id}`} src={MovieImageArr.find(m => m.id === item.imageId)?.image}></img>
                    </div>
                ) : <div>loading ...</div>}
            </div>
        </main>
    )
}

export default RankItems;
import React, { useState, useEffect } from 'react';
import MovieImageArr from './MovieImages';

const RankItems = () => {
    const [items, setItems] = useState([]);

    const dataType = 1;

    useEffect(() => {
        fetch(`item/${dataType}`)
            .then(results => results.json())
            .then(jsonResult => setItems(jsonResult))
    }, [])

    return (
        <main>
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
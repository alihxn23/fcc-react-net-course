
const RankingGrid = ({ items, imgArr }) => {

    const rankingGrid = [];
    const cellCollectionTop = [];
    const cellCollectionMiddle = [];
    const cellCollectionBottom = [];
    const cellCollectionWorst = [];

    const pushCellMarkupToArr = (cellCollection, rankNum, rowLabel) => {
        if (rankNum > 0) {
            let item = items.find(i => i.ranking === rankNum);
            cellCollection.push(<div id={`rank-${rankNum}`} className="rank-cell"></div>)
        }
        else {
            cellCollection.push(<div className="row-label">
                <h4>{rowLabel}</h4>
            </div>)
        }
    }

    const createCellsForRow = (row) => {
        let rankNum = 0;
        let currCollection = [];
        let label = "";
        let numCells = 5;

        for (let i = 1; i <= numCells; i++) {
            rankNum = (i === 1) ? 0 : (numCells * (row - 1)) + i - row;

            switch (row) {
                case 1:
                    currCollection = cellCollectionTop;
                    label = "Top Tier";
                    break;
                case 2:
                    currCollection = cellCollectionMiddle;
                    label = "Middle Tier";
                    break;
                case 3:
                    currCollection = cellCollectionBottom;
                    label = "Bottom Tier";
                    break;
                case 4:
                    currCollection = cellCollectionWorst;
                    label = "Worst Tier";
                    break;
                default:
                    break;
            }

            pushCellMarkupToArr(currCollection, rankNum, label)
        }

    }

    const createCellsForRows = () => {
        const maxRows = 4;
        for (let row = 1; row <= maxRows; row++) {
            createCellsForRow(row)
        }
    }

    const createRowsForGrid = () => {
        rankingGrid.push(<div className="rank-row top-tier">{cellCollectionTop}</div>)
        rankingGrid.push(<div className="rank-row middle-tier">{cellCollectionMiddle}</div>)
        rankingGrid.push(<div className="rank-row bottom-tier">{cellCollectionBottom}</div>)
        rankingGrid.push(<div className="rank-row worst-tier">{cellCollectionWorst}</div>)

        return rankingGrid
    }

    const createRankingGrid = () => {
        createCellsForRows();
        return createRowsForGrid();
    }

    return (
        <div className="rankings">
            {createRankingGrid()}
        </div>
    )
}

export default RankingGrid;
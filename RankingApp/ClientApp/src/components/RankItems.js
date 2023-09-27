import React, { useState, useEffect } from "react";
import ItemCollection from "./ItemCollection";
import RankingGrid from "./RankingGrid";

const RankItems = ({ items, setItems, dataType, imgArr, localStorageKey }) => {
  const [reload, setReload] = useState(false);

  const handleReloadButton = () => {
    setReload(true);
  };

  const drag = (ev) => {
    console.log("from drag");
    ev.dataTransfer.setData("text", ev.target.id);
  };

  const allowDrop = (ev) => {
    console.log("from allowDrop");
    ev.preventDefault();
  };

  const drop = (ev) => {
    console.log("from drop");
    ev.preventDefault();
    const targetElm = ev.target;
    if (targetElm.nodeName === "IMG") {
      return false;
    }
    if (targetElm.childNodes.length === 0) {
      var data = parseInt(ev.dataTransfer.getData("text").substring(5));
      const rank = parseInt(targetElm.id.substring(5));
      const item = items.find((i) => i.id === parseInt(data));

      let changedItem = { id: item.id, ranking: rank };
      console.log("from put method", changedItem);

      var myHeaders = new Headers();
      myHeaders.append("accept", "text/plain");
      myHeaders.append("Content-Type", "application/json");

      var raw = JSON.stringify(changedItem);

      var requestOptions = {
        method: "PUT",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      fetch(`https://localhost:7153/Item`, requestOptions)
        .then((response) => response.json())
        .then((result) => {
          console.log(result);
          const transformedCollection = items.map((item) =>
            item.id === parseInt(data)
              ? { ...item, ranking: parseInt(targetElm.id.substring(5)) }
              : { ...item, ranking: item.ranking }
          );
          setItems(transformedCollection);
        })
        .catch((error) => console.log("error", error));

      return;

      //var data = parseInt(ev.dataTransfer.getData("text").substring(5));
      //const transformedCollection = items.map((item) =>
      //  item.id === parseInt(data)
      //    ? { ...item, ranking: parseInt(targetElm.id.substring(5)) }
      //    : { ...item, ranking: item.ranking }
      //);
      //setItems(transformedCollection);
    }
  };

  useEffect(() => {
    if (items == null) {
      getDataFromApi();
    }
  }, [dataType]);

  const getDataFromApi = () => {
    fetch(`item/${dataType}`)
      .then((results) => {
        return results.json();
      })
      .then((data) => {
        setItems(data);
      });
  };

  useEffect(() => {
    if (items != null) {
      localStorage.setItem(localStorageKey, JSON.stringify(items));
    }
    setReload(false);
  }, [items]);

  useEffect(() => {
    if (reload) {
      var myHeaders = new Headers();
      myHeaders.append("accept", "text/plain");

      var requestOptions = {
        method: "DELETE",
        headers: myHeaders,
        redirect: "follow",
      };

      fetch(`https://localhost:7153/Item/${dataType}`, requestOptions)
        .then((response) => response.json())
        .then((result) => {
          console.log(result);
          setItems(result);
        })
        .catch((error) => console.log("error", error));

      //   getDataFromApi();
    }
  }, [reload]);

  return items != null ? (
    <main>
      <RankingGrid
        items={items}
        imgArr={imgArr}
        drag={drag}
        allowDrop={allowDrop}
        drop={drop}
      />
      <ItemCollection items={items} drag={drag} imgArr={imgArr} />
      <button
        onClick={handleReloadButton}
        className="reload"
        style={{ marginTop: "10px" }}
      >
        <span className="text">Reload</span>
      </button>
    </main>
  ) : (
    <main>Loading...</main>
  );
};

export default RankItems;

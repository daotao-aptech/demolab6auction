import React, {useEffect, useState } from "react";
import axios from "axios";
import * as signalR from "@microsoft/signalr";
import logo from './logo.svg';
import './App.css';




function App() {
  const [auctions, setAuctions] = useState([]);
  const [connection, setConnection] = useState(null);
  const [name, setName] = useState(null);
  useEffect(()=>{
    let n = prompt("nhap ten: ");
    setName(n);
    axios.get("https://glowing-space-fishstick-jjwvr65qr5ppc596v-5297.app.github.dev/api/AuctionItems").then(res => setAuctions(res.data));
  }, []);

  const joinAuctions = async (auctionId) => {
    const conn = new signalR.HubConnectionBuilder().withUrl("https://glowing-space-fishstick-jjwvr65qr5ppc596v-5297.app.github.dev/auctionHub").build();

    conn.on("NewJoin", name=> { console.log(`New bidder joined ${name}`) });

    await conn.start();
    await conn.invoke("JoinAuction", auctionId, name);
    setConnection(conn);
  }



  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>

      <h2>Auction List</h2>
      <ul>
        {auctions.map(a=> (
          <li key={a.id}>
            {a.title} - Current: ${a.currentPrice}
            <button onClick={()=>joinAuctions(a.id)}>Join</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;

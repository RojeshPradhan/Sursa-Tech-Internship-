import React, { useEffect, useState } from "react";
import './App.css';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Nav from "./components/Nav";
import Register from "./pages/Register";
import Home from "./pages/Home";
import Login from "./pages/Login";

function App() {
    const [name, setName] = useState('');

    //useEffect(() => {
    //    (
    //        async () => {
    //            const response = await fetch('http://localhost:5233/api/user', {
    //                headers: {'Content-Type': 'application/json'},
    //                credentials: 'include',
    //            });

    //            const content = await response.json();

    //            setName(content.name);
    //        }
    //    )();
    //});


    return (
        <div className="App">
            <BrowserRouter>
                <Nav name={name} setName={setName}/>

                <main className="container">
                    <Routes>
                        <Route path="/" Component={() => <Home name={name} />} />
                        <Route path="/login" Component={() => <Login setName={setName} />} />
                        <Route path="/register" Component={Register} />
                    </Routes>
                </main>
            </BrowserRouter>
        </div>
    );
}

export default App;

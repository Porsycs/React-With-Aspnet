import $ from 'jquery';
import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [clients, setClients] = useState();

    useEffect(() => {
        populateClientsData();
    }, []);

    const contents = clients === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Document</th>
                    <th>Phone Number</th>
                    <th>Active Client</th>
                </tr>
            </thead>
            <tbody>
                {clients.map(client =>
                    <tr key={client.id}>
                        <td>{client.name}</td>
                        <td>{client.email}</td>
                        <td>{client.document}</td>
                        <td>{client.phone}</td>
                        <td>{client.active ? "YES" : "NO"}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Client DataBase</h1>
            <p>Returns all clients from our database.</p>
            {contents}
        </div>
    );

    function populateClientsData() {
        $.ajax({
            url: 'https://localhost:44374/api/Client/GetAllClients', 
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                setClients(data);
            },
            error: function (xhr, status, error) {
                console.error('error:', status, error);
            }
        });
    }
}

export default App;

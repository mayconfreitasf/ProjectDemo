import './App.css';
import React, {useState} from 'react';
import TemplateCliente from './componets/TemplateCliente';
import ListaCliente from './componets/ListaCliente';
import RoutesApp from './routes';

function App() {
  const [clients, setClients] = useState([]);
  const [editing, setEditing] = useState();

  const getClients = (clientUpdated) => {

    if(clientUpdated.editing){
      let nome = clientUpdated.nome;
      let funcao = clientUpdated.funcao;
      let clientes = clients.map(cliente =>
        cliente.id === clientUpdated.id ? { ...cliente, nome, funcao } : cliente
      );
      setClients(clientes);
    }
    else{
      setClients((prevClientes) =>  [...prevClientes, clientUpdated]);
    }
    
  }

  const editClient = (id) => {
    let client = clients.find(cliente => cliente.id === id);
    client.editing = true;
    setEditing(client);
  };

  const deleteClient = (id) => {
    const clientsUpdated = clients.filter(cliente => cliente.id !== id);
    setClients(clientsUpdated);
  };

return (
    <div className="App">
      <TemplateCliente getClients={getClients} editing={editing}/>
      <hr/>
      <ListaCliente buildClient={clients}
          editClient={editClient}
          deleteClient={deleteClient}/> 
    </div>
  );
}
export default App;
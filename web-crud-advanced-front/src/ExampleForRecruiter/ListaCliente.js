import ItemListaCliente from "./ItemListaCliente"

function ListaCliente({buildClient, editClient, deleteClient}){


    return(
        <div className="ListaCliente">
            <table>
                <tbody>
                <tr>                
                    <th>Id</th>
                    <th>Nome</th>
                    <th>Função</th>
                </tr>

                <ItemListaCliente buildClient={buildClient}
                    editClient={editClient}
                    deleteClient={deleteClient}/> 
                </tbody>
            </table>
        </div>
    )
}

export default ListaCliente

function ItemListaCliente ({buildClient, editClient, deleteClient}){

        const handleEdit = (id, nome, funcao) => {
                editClient(id, nome, funcao);
        };
            
        const handleDelete = (id) => {
                deleteClient(id);
        };      

    return (
        
        buildClient.map((dado, index) => (
                        <tr key={{index}}>
                                <td>{dado.id}</td>
                                <td>{dado.nome}</td>
                                <td>{dado.funcao}</td>
                                <td><button type="button" onClick={() => handleEdit(dado.id)}>Editar</button></td>
                                <td><button type="button" onClick={() => handleDelete(dado.id)}>Excluir</button></td>
                        </tr>
                ))               
    );
}

export default ItemListaCliente
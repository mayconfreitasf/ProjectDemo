import { useEffect, useState } from 'react';

function TemplateCliente({ getClients, editing }){

    const [idCliente, setidCliente] = useState(0);
    const [id, setId] = useState();
    const [nome, setNome] = useState();
    const [funcao, setFuncao] = useState();
    const [isEdit, setIsEdit] = useState();

    useEffect(() => {
        if (editing != null) {
          setId(editing.id);
          setNome(editing.nome);
          setFuncao(editing.funcao);
          setIsEdit(true);
        }
      }, [editing]);

    const Cliente = {
          id: 0,
          nome: '',
          funcao:'',
          editing: false
    };
    const salvarCliente = () => {
        if(Cliente != null){
            Cliente.nome = nome;
            Cliente.funcao = funcao;
            //Novo
            if(editing == null || (isEdit == null || isEdit == false)){
                
                Cliente.id = (idCliente+1);
                Cliente.editing = false;
                setIsEdit(false);
                setidCliente(Cliente.id);
            }
            //Edicao
            else{
                Cliente.id = id;
                Cliente.editing = true;
                setIsEdit(true);
                setId(idCliente+1);
            }

            
            getClients(Cliente);

            setFuncao('');
            setNome('');
            
            setIsEdit(false);
        }
    }

    return(
        <div>
            <form onSubmit={(e) => { e.preventDefault(); salvarCliente();  }}>
                <div><input type="text" id="nome" value={nome} onChange={ (e)=> setNome(e.target.value)}></input></div>
                <div><input type="text" id="funcao" value={funcao} onChange={ (e)=> setFuncao(e.target.value)}></input></div>
                <div><button type="submit">Adicionar</button></div>
            </form>
        </div>
    )
}

export default TemplateCliente
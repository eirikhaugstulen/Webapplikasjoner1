import React, {useEffect, useState} from "react";
import {useLocation} from "react-router-dom";
import axios from "axios";
import {Button, Table} from "reactstrap";
import history from "../history";
import {SlettModal} from "../components/SlettModal";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
}

const fetchEnkeltbillettData = async (id) => {
    return axios.get('/Billett/HentEn', {
        params: {
            id
        }
    }); 
}

const slettEnkeltbillett = async (id) => {
    axios.get('/Billett/Slett', {
        params: {
            id
        }
    })
        .then(() => history.push('/reiser'))
        .catch(e => console.log(e));
}

export const Kvittering = () => {
    const [billett, setBillett] = useState();
    const [modalOpen, setModalOpen] = useState(false)
    const [isLoading, setIsLoading] = useState(true)
    const id = useQuery().get('id');
    
    const toggleModal = () => setModalOpen(prevState => !prevState);
    
    useEffect(() => {
        fetchEnkeltbillettData(id)
            .then(res => {
                setBillett(res.data)
                setIsLoading(false);
            })
            .catch(e => console.log(e))
        
    }, [id])
    
    if (isLoading) {
        return 'Laster inn...'
    }
    
    return ( 
        <div className={'bg-white'}>
            <h2 className={'mb-3'}>Takk for ditt kjøp!</h2>
            <Table className={'table border'}>
                <thead>
                    <tr>
                        <td><h4>Ordredetaljer</h4></td>
                        
                    </tr>
                </thead>
                <tbody className={'table-bordered'}>
                    <tr>
                        <td>Ordrenummer</td>
                        <td>{billett.ordreNummer}</td>
                    </tr>
                    <tr>
                        <td>KundeID</td>
                        <td>{billett.kundeId.kundeId}</td>
                    </tr>
                    <tr>
                        <td>Navn</td>
                        <td>{billett.kundeId.fornavn} {billett.kundeId.etternavn}</td>
                    </tr>
                    <tr>
                        <td>Avreisedato</td>
                        <td>{new Date(Date.parse(`${billett.avgang.dato} ${billett.avgang.klokkeslett}`)).toLocaleDateString()}</td>
                    </tr>
                    <tr>
                        <td>Klokkeslett</td>
                        <td>{billett.avgang.klokkeslett}</td>
                    </tr>
                    <tr>
                        <td>Strekning</td>
                        <td>{billett.avgang.strekning.fraSted.stedsnavn} - {billett.avgang.strekning.tilSted.stedsnavn}</td>
                    </tr>
                    <tr>
                        <td>Pris</td>
                        <td>{billett.totalPris},-</td>
                    </tr>
                </tbody>
            </Table>
            <div
                className={'d-flex'}
                style={{ gap: '10px' }}
            >
                <Button
                    onClick={() => history.push('/reiser')} 
                    color={'secondary'}
                    outline
                >Tilbake</Button>
                <Button
                    color={'danger'}
                    onClick={toggleModal}
                    disabled={(new Date(Date.parse(billett.dato)) < (new Date().setHours(1)))}
                >
                    Kanseller
                </Button>
            </div>
            <SlettModal 
                id={billett.id} 
                isOpen={modalOpen}
                onConfirm={slettEnkeltbillett}
                onCancel={toggleModal}
            />
        </div>
    )
} 
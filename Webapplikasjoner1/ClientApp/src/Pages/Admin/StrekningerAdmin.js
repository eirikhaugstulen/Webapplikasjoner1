import React, {useState} from "react";
import {BackButton} from "../../components/Admin/BackButton";
import {Button, Col, Row, Table} from "reactstrap";
import {LeggTilStrekning} from "../../components/Admin/LeggTil/LeggTilStrekning";
import axios from "axios";
import qs from "qs";
import {checkUnauthorized} from "../../utils/checkUnauthorized";
// Tabell med aktive strekninger (fra databasen)

// Kolonne fralokasjon og tilLokasjon

// Endre
// Slett
// Legg til

export const Strekninger = ({ apiData, refetch }) => {
    const [selectedStrekning, setSelectedStrekning] = useState({
        fraSted: '',
        tilSted: '',
    })
    
    const deleteStrekning = (id) => {
        axios.post('Strekning/Slett', qs.stringify({ id }))
            .then(() => {
                refetch()
            })
            .catch(e => {
                checkUnauthorized(e);
                console.log(e)
            })
    }
return(
    <div>
        <Row className={'p-3'}>
            <BackButton />
        </Row>
        
        <Row>
            <Col md={12}>
                <Table className={'table border'}>
                    <thead className={'thead-light'}>
                        <tr>
                            <td><h4>Strekninger</h4></td>
                        </tr>
                        <tr>
                            <td>Avgangslokasjon</td>
                            <td>Ankomstlokasjon</td>
                            <td>Avganger</td>
                            <td>Slett</td>
                        </tr>
                    </thead>
                    <tbody>
                    {apiData?.strekninger?.map(strekning => (
                        <tr key={strekning.strekningNummer}>
                            <td>
                                <p>{strekning.fraSted.stedsnavn}</p>
                            </td>
                            <td>
                                <p>{strekning.tilSted.stedsnavn}</p>
                            </td>
                            <td>
                                <Button
                                    outline
                                    color={'primary'}
                                >
                                    Avganger
                                </Button>
                            </td>
                            <td>
                                <Button
                                    outline
                                    color={'danger'}
                                    onClick={() => deleteStrekning(strekning.strekningNummer)}
                                >
                                    Slett
                                </Button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            </Col>
        </Row>
        
        <Row className={'mt-3'}>
            <Col>
                <LeggTilStrekning
                    apiData={apiData}
                    refetch={refetch}
                />
            </Col>
        </Row>
    </div>
)
} 
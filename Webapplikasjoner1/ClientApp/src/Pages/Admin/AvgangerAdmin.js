import React, {useMemo} from "react";
import {Button, Col, Row, Table} from "reactstrap";
import {BackButton} from "../../components/Admin/BackButton";
import {useLocationQuery} from "../hooks/useLocationQuery";
import {AvgangsVelger} from "../../components/AvgangsVelger";
import {LeggTilAvgang} from "../../components/Admin/LeggTil/LeggTilAvgang";
import {ref} from "yup";
import axios from "axios";
// Se ALLE avganger

// Filtrere fraLokasjon, tilLokasjon og Dato'

export const Avganger = ({ apiData, refetch }) => {
    const { strekning } = useLocationQuery();
    
    const slettAvgang = (id) => {
        axios.get('/Avganger/Slett?id='+id)
            .then(() => {
                refetch();
            })
    }

    const outputAvganger =
        useMemo(
            () => apiData?.avganger?.filter(
                avgang => avgang.strekning.strekningNummer === strekning), [strekning, apiData.strekninger])

    return(
        <div>
            <Row className={'p-3'}>
                <BackButton />
            </Row>
            <Row>
                <Col>
                    <h4>Avganger</h4>
                </Col>
            </Row>
            <Row>
                <Col md={6}>
                    <AvgangsVelger
                        strekninger={apiData?.strekninger}
                        strekning={strekning}
                    />
                </Col>
            </Row>
            <Row>
                <Col md={12}>
                    {outputAvganger?.length > 0 ? (
                        <Table className={'table border'}>
                            <thead className={'thead-light'}>
                            <tr className={'table-bordered font-weight-bold'}>
                                <td>Avreisested</td>
                                <td>Ankomststed</td>
                                <td>Dato</td>
                                <td>Klokkeslett</td>
                                <td>Grunnpris</td>
                                <td>Slett</td>
                            </tr>
                            </thead>
                            <tbody className={'table-bordered'}>
                            {outputAvganger?.map(avgang => (
                                <tr key={avgang.avgangNummer}>
                                    <td>{avgang.strekning.fraSted.stedsnavn}</td>
                                    <td>{avgang.strekning.tilSted.stedsnavn}</td>
                                    <td>{avgang.dato}</td>
                                    <td>{avgang.klokkeslett}</td>
                                    <td>{avgang.pris},-</td>
                                    <td>
                                        <Button
                                            outline
                                            color={'danger'}
                                            onClick={() => slettAvgang(avgang.avgangNummer)}
                                        >
                                            Slett
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </Table>
                    ) : (
                        <p>Ingen aktive avganger</p>
                    )}
                </Col>
            </Row>

            <Row>
                <Col md={12}>
                    {strekning && (
                        <LeggTilAvgang
                            refetch={refetch}
                            strekning={strekning}
                        />
                    )}
                </Col>
            </Row>
        </div>
    )
} 
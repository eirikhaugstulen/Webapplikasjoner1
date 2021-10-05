import React, {useEffect} from "react";
import {Button, Col, FormFeedback, FormGroup, Input, Label, Row} from "reactstrap";
import {StrekningsVelger} from "../components/BestillForm/StrekningsVelger";
import {useBestillingsForm} from "./hooks/useBestillingsForm";
import history from "../history";

// JSON vil byttes ut med API-kall i del 2
const reiselokasjoner = [
    {
        id: 1,
        displayName: 'Oslo',
    },
    {
        id: 2,
        displayName: 'Kristiansand',
    },
    {
        id: 3,
        displayName: 'Stavanger',
    },
    {
        id: 4,
        displayName: 'Bergen',
    },
    {
        id: 5,
        displayName: 'Ålesund',
    },
    {
        id: 6,
        displayName: 'Trondheim',
    }
]

export const Bestilling = () => {
    const [
        { 
            avgangsstedState, 
            ankomststedState,
            fraDatoState,
            tilDatoState,
            returState,
            fornavnState,
            etternavnState,
            prisState,
        }, 
        valid,
        updateIsTouched,
        handleSubmit,
        submitting,
        isTouched,
    ] = useBestillingsForm();
    
    useEffect(() => {
        if (ankomststedState.ankomststed && avgangsstedState.avgangssted) {
            reiselokasjoner.find(lokasjon => lokasjon.displayName === ankomststedState.ankomststed)
        }
    }, [avgangsstedState, ankomststedState])

    const changeRetur = () => returState.setRetur(!returState.retur);
    
    return (
        <>
            <Row form>
                <Col md={12}>
                    <p>Reisedetaljer</p>
                </Col>
            </Row>
            <Row form>
                <StrekningsVelger
                    reiselokasjoner={reiselokasjoner}
                    avgangsstedState={avgangsstedState}
                    ankomststedState={ankomststedState}
                    returState={returState}
                    updateIsTouched={updateIsTouched}
                    prisState={prisState}
                />
            </Row>
            
            <Row form>
                <Col md={12}>
                    <FormGroup>
                        <Label>Velg avgang</Label>
                        <Input
                            type={'date'}
                            onChange={e => {
                                updateIsTouched('fraDato')
                                fraDatoState.setFraDato(e.target.value)
                            }}
                            invalid={isTouched.fraDato && !fraDatoState.valid}
                        />
                        <FormFeedback>Avreise må være fram i tid</FormFeedback>
                    </FormGroup>
                </Col>
            </Row>
            
            <Row form>
                <Col md={12}>
                    <FormGroup check>
                        <Input type={'checkbox'} onChange={() => changeRetur()} />
                        <Label>Retur?</Label>
                    </FormGroup>
                </Col>
            </Row>

            {returState.retur && (
                <Row form>
                    <Col md={12}>
                        <FormGroup>
                            <Label>Velg returdato</Label>
                            <Input
                                type={'date'}
                                onChange={e => {
                                    updateIsTouched('tilDato')
                                    tilDatoState.setTilDato(e.target.value)
                                }}
                                invalid={isTouched.tilDato && !tilDatoState.valid}
                            />
                            <FormFeedback>Dato kan ikke være før avreise</FormFeedback>
                        </FormGroup>
                    </Col>
                </Row>
            )}
            <Row form>
                <Col md={6}>
                    <h6>Personlig info</h6>
                </Col>
            </Row>
            <Row form>
                <Col md={6}>
                    <FormGroup>
                        <Input 
                            type={'text'} 
                            onChange={(e) => {
                                updateIsTouched('fornavn')
                                fornavnState.setFornavn(e.target.value)
                            }} 
                            placeholder={'Fornavn'}
                            invalid={isTouched.fornavn && !fornavnState.valid}
                        />
                        <FormFeedback>Vennligst skriv inn et gyldig fornavn</FormFeedback>
                    </FormGroup>
                </Col>
                <Col md={6}>
                    <FormGroup>
                        <Input 
                            type={'text'} 
                            onChange={(e) => {
                                updateIsTouched('etternavn')
                                etternavnState.setEtternavn(e.target.value)
                            }} 
                            placeholder={'Etternavn'}
                            invalid={isTouched.etternavn && !etternavnState.valid}
                        />
                        <FormFeedback>Vennligst skriv inn et gyldig etternavn</FormFeedback>
                    </FormGroup>
                </Col>
            </Row>
            <Row>
                <Col md={12}>
                    <h3>Ordredetaljer:</h3>
                    {(isTouched.avgangssted && isTouched.ankomststed) && <p>Strekning: {avgangsstedState?.avgangssted} - {ankomststedState?.ankomststed}</p>}
                    <h5>{prisState.pris && (`Pris:  ${prisState.pris},-`)}</h5>
                </Col>
            </Row>
            <div className={'mt-3 d-flex'} style={{gap: '5px'}}>
                <Button
                    color={'primary'}
                    disabled={!valid}
                    onClick={handleSubmit}
                >
                    {submitting ? 'Sender...' : 'Bestill'}
                </Button>
                <Button
                    color={'secondary'}
                    onClick={() => history.push("/")}
                    outline
                >
                    Avbryt
                </Button>
            </div>
        </>
    )
}
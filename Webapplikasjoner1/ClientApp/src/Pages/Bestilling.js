import React, {useState} from "react";
import {Button, Col, Form, FormGroup, Input, Label, Row} from "reactstrap";
import {StrekningsVelger} from "../components/BestillForm/StrekningsVelger";
import {useBestillingsForm} from "./hooks/useBestillingsForm";

export const Bestilling = () => {
    const [
        { 
            avgangsstedState, 
            ankomststedState,
            fraDatoState,
            tilDatoState,
            returState,
        }, 
        valid,
        updateIsTouched,
        handleSubmit,
    ] = useBestillingsForm();

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
                    avgangsstedState={avgangsstedState}
                    ankomststedState={ankomststedState}
                    updateIsTouched={updateIsTouched}
                />
            </Row>
            
            <Row form>
                <Col md={12}>
                    <FormGroup check>
                        <Input type={'checkbox'} onChange={() => changeRetur()} />
                        <Label>Tur/Retur?</Label>
                    </FormGroup>
                </Col>
            </Row>

            <Row form>
                <Col md={12}>
                    <FormGroup>
                        <Label>Velg avgang</Label>
                        <Input
                            type={'date'}
                            onBlur={e => fraDatoState.setFraDato(e.target.value)}
                        />
                    </FormGroup>
                </Col>
            </Row>

            <Row form>
                <Col md={12}>
                    <FormGroup>
                        <Label>Velg retur</Label>
                        <Input 
                            type={'date'} 
                            disabled={!returState.retur}
                            onBlur={e => tilDatoState.setTilDato(e.target.value)}
                        />
                    </FormGroup>
                </Col>
            </Row>

            <div className={'mt-3'}>
                <Button
                    color={'primary'}
                    disabled={!valid}
                    onClick={handleSubmit}
                >
                    Bestill
                </Button>
            </div>
        </>
    )
}
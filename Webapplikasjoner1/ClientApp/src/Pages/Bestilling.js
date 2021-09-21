import React, {useState} from "react";
import {Button, Col, Form, FormGroup, Input, Label, Row} from "reactstrap";
import {StrekningsVelger} from "../components/BestillForm/StrekningsVelger";

export const Bestilling = () => {
    const [avgangssted, setAvgangssted] = useState('default');
    const [fraDato, setFraDato] = useState();
    const [tilDato, setTilDato] = useState();
    const [retur, setRetur] = useState(false);
    const [loading, setLoading] = useState(true);

    const changeRetur = () => setRetur(!retur);
    
    return (
        <Form>
            <Row form>
                <Col md={12}>
                    <p>Reisedetaljer</p>
                </Col>
            </Row>
            <Row form>
                <StrekningsVelger
                    avgangssted={avgangssted}
                    setAvgangssted={setAvgangssted}
                    setLoading={setLoading}
                    loading={loading}
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
                        <Input type={'date'} />
                    </FormGroup>
                </Col>
            </Row>

            <Row form>
                <Col md={12}>
                    <FormGroup>
                        <Label>Velg retur</Label>
                        <Input type={'date'} disabled={!retur}/>
                    </FormGroup>
                </Col>
            </Row>

            <div className={'mt-3'}>
                <Button
                    color={'primary'}
                >
                    Neste
                </Button>
            </div>
        </Form>
    )
}
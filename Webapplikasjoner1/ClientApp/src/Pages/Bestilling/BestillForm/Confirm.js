import React from "react";
import {Button, Col, Row} from "reactstrap";

export const Confirm = ({ confirm }) => (
    <Row>
        <Col md={3} />
        <Col md={6} className={'text-center mt-5 p-4 border'}>
            <h4>Confirm</h4>
            <p>Er du sikker på at du vil kjøpe <b>4</b> billetter til avgangen mellom <b>Trondheim</b> og <b>Molde</b>?</p>
            <Button
                outline
                color={'primary'}
                onClick={confirm}
            >Bestill</Button>
        </Col>
        <Col md={3} />
    </Row>
)
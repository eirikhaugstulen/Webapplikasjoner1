import React, {useEffect, useState} from "react";
import {Button, Col, Progress, Row} from "reactstrap";
import axios from "axios";
import {LoadingScreen} from "../../components/LoadingScreen";
import {ErrorScreen} from "../../components/ErrorScreen";
import {BestillForm} from "./BestillForm/BestillForm";
import {SelectAvgang} from "./BestillForm/SelectAvgang";

const fetchLokasjoner = async () => axios.get('/Lokasjon/HentAlle');
const fetchAvganger = async () => axios.get('/Avganger/HentAlle');

export const Bestilling = () => {
    const [lokasjoner, setLokasjoner] = useState([]);
    const [avganger, setAvganger] = useState([])
    const [step, setStep] = useState(0);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState();
    
    useEffect(() => {
        Promise.all([fetchLokasjoner(), fetchAvganger()])
            .then(res => {
                setLokasjoner(res[0].data);
                setAvganger(res[1].data);
                setLoading(false);
            })
            .catch(e => {
                console.log(e);
                setError(e);
            })
    }, [])
    
    const progress = [0, 15, 30];

    if (error) {
        return ( <ErrorScreen error={error} /> );
    }

    if (loading) {
        return ( <LoadingScreen /> );
    }
    
    return (
        <>
            <Row form>
                <Col md={12}>
                    <p>Bestill reise</p>
                    <Progress multi>
                        <Progress
                            animated
                            bar
                            color={'info'}
                            value={progress[step]}
                        />
                    </Progress>
                    {step > 0 && (
                        <div className="mt-3">
                            <Button
                                outline
                                onClick={() => setStep(prevState => prevState - 1)}
                            >
                                Tilbake
                            </Button>
                        </div>
                    )}
                </Col>
            </Row>

            {step === 0 && (
                <BestillForm
                    lokasjoner={lokasjoner}
                    setStep={setStep}
                />
            )}

            {step === 1 && (
                <SelectAvgang 
                    avganger={avganger} 
                />
            )}
        </>
    )
}

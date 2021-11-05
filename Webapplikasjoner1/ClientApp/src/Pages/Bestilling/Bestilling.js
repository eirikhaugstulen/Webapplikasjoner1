import React, {useEffect, useState} from "react";
import {Button, Col, Progress, Row} from "reactstrap";
import axios from "axios";
import {LoadingScreen} from "../../components/LoadingScreen";
import {ErrorScreen} from "../../components/ErrorScreen";
import {BestillForm} from "./BestillForm/BestillForm";
import {SelectAvgang} from "./BestillForm/SelectAvgang";
import {Personalia} from "./BestillForm/Personalia";
import {Pris} from "./BestillForm/Pris";
import {Confirm} from "./BestillForm/Confirm";
import qs from "qs";
import {useGeneratedId} from "../hooks/useGeneratedId";
import history from "../../history";

const fetchLokasjoner = async () => axios.get('/Lokasjon/HentAlle');
const fetchAvganger = async () => axios.get('/Avganger/HentAlle');

export const Bestilling = () => {
    const [lokasjoner, setLokasjoner] = useState([]);
    const [avganger, setAvganger] = useState([])
    const [step, setStep] = useState(0);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState();
    const { generatedId: BillettId } = useGeneratedId();
    const { generatedId: returId } = useGeneratedId();
    
    const [BillettKundeId, setBillettKundeId] = useState('');
    const [avgangssted, setAvgangssted] = useState('');
    const [ankomststed, setAnkomssted] = useState('')
    const [retur, setRetur] = useState(false);
    const [fraDato, setFraDato] = useState();
    const [fraKlokkeslett, setFraKlokkeslett] = useState();
    const [tilDato, setTilDato] = useState();
    const [tilKlokkeslett, setTilKlokkeslett] = useState();
    const [totalPris, setTotalpris] = useState(0);
    const [totaltAntallBilletter, setTotaltAntallBilletter] = useState(0)
    
    const [fraAvgang, setFraAvgang] = useState();
    const [tilAvgang, setTilAvgang] = useState();

    const sendBillett = async () => {
        let ApiKall = [];
        ApiKall.push(axios.post('Billett/Lagre', qs.stringify({
            Retur: retur,
            TotalPris: totalPris,
            Antall: totaltAntallBilletter,
            OrdreNummer: BillettId,
            KundeId: BillettKundeId,
            Avgang: fraAvgang.avgangNummer,
        })))
        
        if (retur) {
            ApiKall.push(axios.post('Billett/Lagre', qs.stringify({
                Retur: retur,
                TotalPris: totalPris,
                Antall: totaltAntallBilletter,
                OrdreNummer: returId,
                KundeId: BillettKundeId,
                Avgang: tilAvgang.avgangNummer,
            })))
        }
        
        Promise.all(ApiKall)
            .then(res => {
                console.log(res);
                setStep(6);
            })
            .catch(e => console.log(e))
    }

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
    
    const progress = retur ? [5, 15, 20, 30, 60, 80, 100] : [5, 20, 30, 40, 60, 80, 100];

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
                    {/*{step > 0 && (
                        <div className="mt-3">
                            <Button
                                outline
                                onClick={() => setStep(prevState => prevState - 1)}
                            >
                                Tilbake
                            </Button>
                        </div>
                    )}*/}
                </Col>
            </Row>

            {step === 0 && (
                <BestillForm
                    lokasjoner={lokasjoner}
                    avgangssted={avgangssted}
                    setAvgangssted={setAvgangssted}
                    ankomststed={ankomststed}
                    setAnkomststed={setAnkomssted}
                    retur={retur}
                    setRetur={setRetur}
                    fraDato={fraDato}
                    setFraDato={setFraDato}
                    fraKlokkeslett={fraKlokkeslett}
                    setFraKlokkeslett={setFraKlokkeslett}
                    tilDato={tilDato}
                    tilKlokkeslett={tilKlokkeslett}
                    setTilKlokkeslett={setTilKlokkeslett}
                    setTilDato={setTilDato}
                    setStep={setStep}
                />
            )}

            {step === 1 && (
                <SelectAvgang 
                    avganger={avganger}
                    avgangssted={avgangssted}
                    ankomststed={ankomststed}
                    dato={fraDato}
                    klokkeslett={fraKlokkeslett}
                    selectAvgang={setFraAvgang}
                    skipStep={retur ? 1 : 2}
                    setStep={setStep}
                />
            )}

            {(step === 2 && retur) && (
                <SelectAvgang 
                    avganger={avganger}
                    avgangssted={ankomststed}
                    ankomststed={avgangssted}
                    klokkeslett={tilKlokkeslett}
                    dato={tilDato}
                    selectAvgang={setTilAvgang}
                    skipStep={1}
                    setStep={setStep}
                />
            )}

            {step === 3 && (
                <Personalia 
                    setBillettKundeId={setBillettKundeId}
                    setStep={setStep}
                />
            )}

            {step === 4 && (
                <Pris 
                    fraAvgangPris={fraAvgang?.pris}
                    tilAvgangPris={tilAvgang?.pris}
                    setTotalPris={setTotalpris}
                    setTotaltAntallBilletter={setTotaltAntallBilletter}
                    setStep={setStep}
                />
            )}

            {step === 5 && (
                <Confirm confirm={sendBillett} />
            )}

            {step === 6 && (
                <Row>
                    <Col md={3} />
                    <Col md={6} className={'border mt-4 p-4 text-center'}>
                        <h4>Takk for din bestilling!</h4>
                        <p>Du er nå klar for å reise med oss i AnvendtLine</p>
                        <Button
                            outline
                            color={'primary'}
                            size={'sm'}
                            onClick={() => history.push('/reiser')}
                        >
                            Dine billetter
                        </Button>
                    </Col>
                    <Col md={3} />
                </Row>
            )}
        </>
    )
}

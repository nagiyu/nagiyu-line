using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Line.Models;

namespace Line.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var privacies = new List<Privacy>
            {
                new Privacy
                {
                    Title = "個人情報",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "「個人情報」とは，個人情報保護法にいう「個人情報」を指すものとし，生存する個人に関する情報であって，当該情報に含まれる氏名，生年月日，住所，電話番号，連絡先その他の記述等により特定の個人を識別できる情報及び容貌，指紋，声紋にかかるデータ，及び健康保険証の保険者番号などの当該情報単体から特定の個人を識別できる情報（個人識別情報）を指します。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "個人情報の収集方法",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，ユーザーが利用登録をする際に氏名，生年月日，住所，電話番号，メールアドレス，銀行口座番号，クレジットカード番号，運転免許証番号などの個人情報をお尋ねすることがあります。また，ユーザーと提携先などとの間でなされたユーザーの個人情報を含む取引記録や決済に関する情報を,なぎゆーの提携先（情報提供元，広告主，広告配信先などを含みます。以下，｢提携先｣といいます。）などから収集することがあります。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "個人情報を収集・利用する目的",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーが個人情報を収集・利用する目的は，以下のとおりです。",
                            SubContents = new List<PrivacySubContent>
                            {
                                new PrivacySubContent
                                {
                                    SubContent = "なぎゆーサービスの提供・運営のため",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "ユーザーからのお問い合わせに回答するため（本人確認を行うことを含む）",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "ユーザーが利用中のサービスの新機能，更新情報，キャンペーン等及びなぎゆーが提供する他のサービスの案内のメールを送付するため",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "メンテナンス，重要なお知らせなど必要に応じたご連絡のため",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "利用規約に違反したユーザーや，不正・不当な目的でサービスを利用しようとするユーザーの特定をし，ご利用をお断りするため",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "ユーザーにご自身の登録情報の閲覧や変更，削除，ご利用状況の閲覧を行っていただくため",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "有料サービスにおいて，ユーザーに利用料金を請求するため",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "上記の利用目的に付随する目的",
                                    SubItems = null
                                }
                            }
                        }
                    }
                },
                new Privacy
                {
                    Title = "利用目的の変更",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，利用目的が変更前と関連性を有すると合理的に認められる場合に限り，個人情報の利用目的を変更するものとします。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "利用目的の変更を行った場合には，変更後の目的について，なぎゆー所定の方法により，ユーザーに通知し，または本ウェブサイト上に公表するものとします。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "個人情報の第三者提供",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，次に掲げる場合を除いて，あらかじめユーザーの同意を得ることなく，第三者に個人情報を提供することはありません。ただし，個人情報保護法その他の法令で認められる場合を除きます。",
                            SubContents = new List<PrivacySubContent>
                            {
                                new PrivacySubContent
                                {
                                    SubContent = "人の生命，身体または財産の保護のために必要がある場合であって，本人の同意を得ることが困難であるとき",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "公衆衛生の向上または児童の健全な育成の推進のために特に必要がある場合であって，本人の同意を得ることが困難であるとき",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "国の機関もしくは地方公共団体またはその委託を受けた者が法令の定める事務を遂行することに対して協力する必要がある場合であって，本人の同意を得ることにより当該事務の遂行に支障を及ぼすおそれがあるとき",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "予め次の事項を告知あるいは公表し，かつなぎゆーが個人情報保護委員会に届出をしたとき",
                                    SubItems = new List<string>
                                    {
                                        "利用目的に第三者への提供を含むこと",
                                        "第三者に提供されるデータの項目",
                                        "第三者への提供の手段または方法",
                                        "本人の求めに応じて個人情報の第三者への提供を停止すること",
                                        "本人の求めを受け付ける方法"
                                    }
                                }
                            }
                        },
                        new PrivacyContent
                        {
                            MainContent = "前項の定めにかかわらず，次に掲げる場合には，当該情報の提供先は第三者に該当しないものとします。",
                            SubContents = new List<PrivacySubContent>
                            {
                                new PrivacySubContent
                                {
                                    SubContent = "なぎゆーが利用目的の達成に必要な範囲内において個人情報の取扱いの全部または一部を委託する場合",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "合併その他の事由による事業の承継に伴って個人情報が提供される場合",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "個人情報を特定の者との間で共同して利用する場合であって，その旨並びに共同して利用される個人情報の項目，共同して利用する者の範囲，利用する者の利用目的および当該個人情報の管理について責任を有する者の氏名または名称について，あらかじめ本人に通知し，または本人が容易に知り得る状態に置いた場合",
                                    SubItems = null
                                }
                            }
                        }
                    }
                },
                new Privacy
                {
                    Title = "個人情報の開示",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，本人から個人情報の開示を求められたときは，本人に対し，遅滞なくこれを開示します。ただし，開示することにより次のいずれかに該当する場合は，その全部または一部を開示しないこともあり，開示しない決定をした場合には，その旨を遅滞なく通知します。なお，個人情報の開示に際しては，1件あたり1，000円の手数料を申し受けます。",
                            SubContents = new List<PrivacySubContent>
                            {
                                new PrivacySubContent
                                {
                                    SubContent = "本人または第三者の生命，身体，財産その他の権利利益を害するおそれがある場合",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "なぎゆーの業務の適正な実施に著しい支障を及ぼすおそれがある場合",
                                    SubItems = null
                                },
                                new PrivacySubContent
                                {
                                    SubContent = "その他法令に違反することとなる場合",
                                    SubItems = null
                                }
                            }
                        },
                        new PrivacyContent
                        {
                            MainContent = "前項の定めにかかわらず，履歴情報および特性情報などの個人情報以外の情報については，原則として開示いたしません。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "個人情報の訂正および削除",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "ユーザーは，なぎゆーの保有する自己の個人情報が誤った情報である場合には，なぎゆーが定める手続きにより，なぎゆーに対して個人情報の訂正，追加または削除（以下，「訂正等」といいます。）を請求することができます。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，ユーザーから前項の請求を受けてその請求に応じる必要があると判断した場合には，遅滞なく，当該個人情報の訂正等を行うものとします。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，前項の規定に基づき訂正等を行った場合，または訂正等を行わない旨の決定をしたときは遅滞なく，これをユーザーに通知します。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "個人情報の利用停止等",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，本人から，個人情報が，利用目的の範囲を超えて取り扱われているという理由，または不正の手段により取得されたものであるという理由により，その利用の停止または消去（以下，「利用停止等」といいます。）を求められた場合には，遅滞なく必要な調査を行います。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "前項の調査結果に基づき，その請求に応じる必要があると判断した場合には，遅滞なく，当該個人情報の利用停止等を行います。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーは，前項の規定に基づき利用停止等を行った場合，または利用停止等を行わない旨の決定をしたときは，遅滞なく，これをユーザーに通知します。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "前2項にかかわらず，利用停止等に多額の費用を有する場合その他利用停止等を行うことが困難な場合であって，ユーザーの権利利益を保護するために必要なこれに代わるべき措置をとれる場合は，この代替策を講じるものとします。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "プライバシーポリシーの変更",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "本ポリシーの内容は，法令その他本ポリシーに別段の定めのある事項を除いて，ユーザーに通知することなく，変更することができるものとします。",
                            SubContents = null
                        },
                        new PrivacyContent
                        {
                            MainContent = "なぎゆーが別途定める場合を除いて，変更後のプライバシーポリシーは，本ウェブサイトに掲載したときから効力を生じるものとします。",
                            SubContents = null
                        }
                    }
                },
                new Privacy
                {
                    Title = "お問い合わせ窓口",
                    Contents = new List<PrivacyContent>
                    {
                        new PrivacyContent
                        {
                            MainContent = "本ポリシーに関するお問い合わせは，下記までお願いいたします。",
                            SubContents = null,
                            Link = "https://forms.gle/oxzHNFBWBpFGNaKm7"
                        }
                    }
                }
            };

            return View(privacies);
        }

        public IActionResult Terms()
        {
            var terms = new List<Term>
            {
                new Term
                {
                    Title = "適用",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "本規約は，ユーザーとなぎゆーとの間の本サービスの利用に関わる一切の関係に適用されるものとします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーは本サービスに関し，本規約のほか，ご利用にあたってのルール等，各種の定め（以下，「個別規定」といいます。）をすることがあります。これら個別規定はその名称のいかんに関わらず，本規約の一部を構成するものとします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "本規約の規定が前条の個別規定の規定と矛盾する場合には，個別規定において特段の定めなき限り，個別規定の規定が優先されるものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "利用登録",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "本サービスにおいては，登録希望者が本規約に同意の上，なぎゆーの定める方法によって利用登録を申請し，なぎゆーがこれを承認することによって，利用登録が完了するものとします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーは，利用登録の申請者に以下の事由があると判断した場合，利用登録の申請を承認しないことがあり，その理由については一切の開示義務を負わないものとします。",
                            SubItems = new List<string>
                            {
                                "利用登録の申請に際して虚偽の事項を届け出た場合",
                                "本規約に違反したことがある者からの申請である場合",
                                "その他，なぎゆーが利用登録を相当でないと判断した場合"
                            }
                        }
                    }
                },
                new Term
                {
                    Title = "ユーザーIDおよびパスワードの管理",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "ユーザーは，自己の責任において，本サービスのユーザーIDおよびパスワードを適切に管理するものとします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "ユーザーは，いかなる場合にも，ユーザーIDおよびパスワードを第三者に譲渡または貸与し，もしくは第三者と共用することはできません。なぎゆーは，ユーザーIDとパスワードの組み合わせが登録情報と一致してログインされた場合には，そのユーザーIDを登録しているユーザー自身による利用とみなします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "ユーザーID及びパスワードが第三者によって使用されたことによって生じた損害は，なぎゆーに故意又は重大な過失がある場合を除き，なぎゆーは一切の責任を負わないものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "利用料金および支払方法",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "ユーザーは，本サービスの有料部分の対価として，なぎゆーが別途定め，本ウェブサイトに表示する利用料金を，なぎゆーが指定する方法により支払うものとします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "ユーザーが利用料金の支払を遅滞した場合には，ユーザーは年14．6％の割合による遅延損害金を支払うものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "禁止事項",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "ユーザーは，本サービスの利用にあたり，以下の行為をしてはなりません。",
                            SubItems = new List<string>
                            {
                                "法令または公序良俗に違反する行為",
                                "犯罪行為に関連する行為",
                                "本サービスの内容等，本サービスに含まれる著作権，商標権ほか知的財産権を侵害する行為",
                                "なぎゆー，ほかのユーザー，またはその他第三者のサーバーまたはネットワークの機能を破壊したり，妨害したりする行為",
                                "本サービスによって得られた情報を商業的に利用する行為",
                                "なぎゆーのサービスの運営を妨害するおそれのある行為",
                                "不正アクセスをし，またはこれを試みる行為",
                                "他のユーザーに関する個人情報等を収集または蓄積する行為",
                                "不正な目的を持って本サービスを利用する行為",
                                "本サービスの他のユーザーまたはその他の第三者に不利益，損害，不快感を与える行為",
                                "他のユーザーに成りすます行為",
                                "なぎゆーが許諾しない本サービス上での宣伝，広告，勧誘，または営業行為",
                                "面識のない異性との出会いを目的とした行為",
                                "なぎゆーのサービスに関連して，反社会的勢力に対して直接または間接に利益を供与する行為",
                                "その他，なぎゆーが不適切と判断する行為"
                            }
                        }
                    }
                },
                new Term
                {
                    Title = "本サービスの提供の停止等",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "なぎゆーは，以下のいずれかの事由があると判断した場合，ユーザーに事前に通知することなく本サービスの全部または一部の提供を停止または中断することができるものとします。",
                            SubItems = new List<string>
                            {
                                "本サービスにかかるコンピュータシステムの保守点検または更新を行う場合",
                                "地震，落雷，火災，停電または天災などの不可抗力により，本サービスの提供が困難となった場合",
                                "コンピュータまたは通信回線等が事故により停止した場合",
                                "その他，なぎゆーが本サービスの提供が困難と判断した場合"
                            }
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーは，本サービスの提供の停止または中断により，ユーザーまたは第三者が被ったいかなる不利益または損害についても，一切の責任を負わないものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "利用制限および登録抹消",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "なぎゆーは，ユーザーが以下のいずれかに該当する場合には，事前の通知なく，ユーザーに対して，本サービスの全部もしくは一部の利用を制限し，またはユーザーとしての登録を抹消することができるものとします。",
                            SubItems = new List<string>
                            {
                                "本規約のいずれかの条項に違反した場合",
                                "登録事項に虚偽の事実があることが判明した場合",
                                "料金等の支払債務の不履行があった場合",
                                "なぎゆーからの連絡に対し，一定期間返答がない場合",
                                "本サービスについて，最終の利用から一定期間利用がない場合",
                                "その他，なぎゆーが本サービスの利用を適当でないと判断した場合"
                            }
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーは，本条に基づきなぎゆーが行った行為によりユーザーに生じた損害について，一切の責任を負いません。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "退会",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "ユーザーは，なぎゆーの定める退会手続により，本サービスから退会できるものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "保証の否認および免責事項",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "なぎゆーは，本サービスに事実上または法律上の瑕疵（安全性，信頼性，正確性，完全性，有効性，特定の目的への適合性，セキュリティなどに関する欠陥，エラーやバグ，権利侵害などを含みます。）がないことを明示的にも黙示的にも保証しておりません。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーは，本サービスに起因してユーザーに生じたあらゆる損害について、なぎゆーの故意又は重過失による場合を除き、一切の責任を負いません。ただし，本サービスに関するなぎゆーとユーザーとの間の契約（本規約を含みます。）が消費者契約法に定める消費者契約となる場合，この免責規定は適用されません。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "前項ただし書に定める場合であっても，なぎゆーは，なぎゆーの過失（重過失を除きます。）による債務不履行または不法行為によりユーザーに生じた損害のうち特別な事情から生じた損害（なぎゆーまたはユーザーが損害発生につき予見し，または予見し得た場合を含みます。）について一切の責任を負いません。また，なぎゆーの過失（重過失を除きます。）による債務不履行または不法行為によりユーザーに生じた損害の賠償は，ユーザーから当該損害が発生した月に受領した利用料の額を上限とします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーは，本サービスに関して，ユーザーと他のユーザーまたは第三者との間において生じた取引，連絡または紛争等について一切責任を負いません。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "サービス内容の変更等",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "なぎゆーは，ユーザーへの事前の告知をもって、本サービスの内容を変更、追加または廃止することがあり、ユーザーはこれを承諾するものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "利用規約の変更",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "なぎゆーは以下の場合には、ユーザーの個別の同意を要せず、本規約を変更することができるものとします。",
                            SubItems = new List<string>
                            {
                                "本規約の変更がユーザーの一般の利益に適合するとき。",
                                "本規約の変更が本サービス利用契約の目的に反せず、かつ、変更の必要性、変更後の内容の相当性その他の変更に係る事情に照らして合理的なものであるとき。"
                            }
                        },
                        new TermContent
                        {
                            MainContent = "なぎゆーはユーザーに対し、前項による本規約の変更にあたり、事前に、本規約を変更する旨及び変更後の本規約の内容並びにその効力発生時期を通知します。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "個人情報の取扱い",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "なぎゆーは，本サービスの利用によって取得する個人情報については，なぎゆー「プライバシーポリシー」に従い適切に取り扱うものとします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "通知または連絡",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "ユーザーとなぎゆーとの間の通知または連絡は，なぎゆーの定める方法によって行うものとします。なぎゆーは,ユーザーから,なぎゆーが別途定める方式に従った変更届け出がない限り,現在登録されている連絡先が有効なものとみなして当該連絡先へ通知または連絡を行い,これらは,発信時にユーザーへ到達したものとみなします。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "権利義務の譲渡の禁止",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "ユーザーは，なぎゆーの書面による事前の承諾なく，利用契約上の地位または本規約に基づく権利もしくは義務を第三者に譲渡し，または担保に供することはできません。",
                            SubItems = null
                        }
                    }
                },
                new Term
                {
                    Title = "準拠法・裁判管轄",
                    Contents = new List<TermContent>
                    {
                        new TermContent
                        {
                            MainContent = "本規約の解釈にあたっては，日本法を準拠法とします。",
                            SubItems = null
                        },
                        new TermContent
                        {
                            MainContent = "本サービスに関して紛争が生じた場合には，なぎゆーの本店所在地を管轄する裁判所を専属的合意管轄とします。",
                            SubItems = null
                        }
                    }
                }
            };

            return View(terms);
        }

        public IActionResult HowToUse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

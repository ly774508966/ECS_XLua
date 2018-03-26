HitFactroy = { }

local hitMap = { }
hitMap[HitConst.normal] = NormalHit
hitMap[HitConst.moving] = MovingHit
hitMap[HitConst.bullet] = BulletHit

--只有本机玩家计算是否攻击到敌人
function HitFactroy:hit(cfgId,roleId)
    local hitConf = ConfigHelper:getConfigByKey('HitConfig',cfgId)
    if hitConf then 
    	local type = hitConf.type 
    	local creator = hitMap[type] and hitMap[type] or BaseHit
    	local calss = creator(hitConf,roleId)
    	calss:onExecute()
    end 
end